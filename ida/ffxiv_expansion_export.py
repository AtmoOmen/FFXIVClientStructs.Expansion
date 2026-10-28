from __future__ import annotations

import argparse
import copy
import hashlib
import json
import re
import sys
from collections import defaultdict
from pathlib import Path
from xml.etree import ElementTree

import dnfile
import yaml


class Reader:
    def __init__(self, data):
        self.data = data
        self.offset = 0

    def take(self, length):
        result = self.data[self.offset : self.offset + length]
        if len(result) != length:
            raise ValueError("程序集元数据不完整")
        self.offset += length
        return result

    def integer(self, length=1, signed=False):
        return int.from_bytes(self.take(length), "little", signed=signed)

    def compressed(self):
        first = self.integer()
        if first < 0x80:
            return first
        if first < 0xC0:
            return ((first & 0x3F) << 8) | self.integer()
        return ((first & 0x1F) << 24) | int.from_bytes(self.take(3), "big")

    def string(self):
        if self.data[self.offset] == 0xFF:
            self.offset += 1
            return None
        return self.take(self.compressed()).decode("utf-8")


class Metadata:
    def __init__(self, assembly):
        self.path = Path(assembly).resolve()
        self.pe = dnfile.dnPE(str(self.path))
        self.tables = self.pe.net.mdtables
        self.parents = {
            id(row.NestedClass.row): row.EnclosingClass.row
            for row in self.rows("NestedClass")
        }
        self.layouts = {
            id(row.Parent.row): row.ClassSize for row in self.rows("ClassLayout")
        }
        self.offsets = {
            id(row.Field.row): row.Offset for row in self.rows("FieldLayout")
        }
        self.constants = {id(row.Parent.row): row for row in self.rows("Constant")}
        self.attributes = defaultdict(list)
        for attribute in self.rows("CustomAttribute"):
            constructor = attribute.Type.row
            owner = getattr(constructor, "Class", None)
            if owner is not None:
                name = self.token_name(owner)
                if name.split("<", 1)[0].rsplit(".", 1)[-1].split("`")[0] in {
                    "InheritsAttribute",
                    "MemberFunctionAttribute",
                    "StaticAddressAttribute",
                    "VirtualFunctionAttribute",
                    "FixedSizeArrayAttribute",
                    "FixedBufferAttribute",
                    "BitFieldAttribute",
                    "VirtualTableAttribute",
                    "CompilerGeneratedAttribute",
                    "CExporterIgnoreAttribute",
                    "FlagsAttribute",
                }:
                    self.attributes[id(attribute.Parent.row)].append(
                        (name, constructor, attribute.Value.value)
                    )
        self.docs = {}
        xml = self.path.with_suffix(".xml")
        if xml.exists():
            for member in ElementTree.parse(xml).findall("./members/member"):
                self.docs[member.attrib["name"]] = " ".join(
                    "".join(member.itertext()).split()
                )

    def rows(self, name):
        table = getattr(self.tables, name, None)
        return table.rows if table is not None else ()

    def row_name(self, row):
        if id(row) in self.parents:
            return self.row_name(self.parents[id(row)]) + "." + str(row.TypeName)
        namespace = str(row.TypeNamespace)
        scope = getattr(row, "ResolutionScope", None)
        if (
            not namespace
            and scope is not None
            and scope.table is not None
            and scope.table.name == "TypeRef"
        ):
            return self.row_name(scope.row) + "." + str(row.TypeName)
        return namespace + "." + str(row.TypeName) if namespace else str(row.TypeName)

    def token_name(self, token):
        if token.table.name == "TypeSpec":
            return self.type_name(Reader(token.row.Signature.value))
        return self.row_name(token.row)

    def type_name(self, reader):
        kind = reader.integer()
        primitives = {
            0x01: "void",
            0x02: "bool",
            0x03: "wchar_t",
            0x04: "__int8",
            0x05: "byte",
            0x06: "__int16",
            0x07: "unsigned __int16",
            0x08: "int",
            0x09: "unsigned int",
            0x0A: "__int64",
            0x0B: "unsigned __int64",
            0x0C: "float",
            0x0D: "double",
            0x0E: "string",
            0x18: "__int64",
            0x19: "unsigned __int64",
            0x1C: "object",
        }
        if kind in primitives:
            return primitives[kind]
        if kind in (0x1F, 0x20):
            reader.compressed()
            return self.type_name(reader)
        if kind in (0x0F, 0x10):
            return self.type_name(reader) + "*"
        if kind == 0x1D:
            return self.type_name(reader) + "[]"
        if kind in (0x11, 0x12):
            token = reader.compressed()
            table = (self.tables.TypeDef, self.tables.TypeRef, self.tables.TypeSpec)[
                token & 3
            ]
            row = table.rows[(token >> 2) - 1]
            return (
                self.type_name(Reader(row.Signature.value))
                if token & 3 == 2
                else self.row_name(row)
            )
        if kind == 0x15:
            name = self.type_name(reader)
            arguments = [self.type_name(reader) for _ in range(reader.compressed())]
            return name + "<" + ", ".join(arguments) + ">"
        if kind in (0x13, 0x1E):
            return "T" + str(reader.compressed())
        if kind == 0x1B:
            result, parameters = self.method_signature(reader)
            return result + " (*) (" + ", ".join(parameters) + ")"
        raise ValueError(f"不支持的元数据类型：{kind:#x}")

    def method_signature(self, reader):
        flags = reader.integer()
        if flags & 0x10:
            reader.compressed()
        count = reader.compressed()
        result = self.type_name(reader)
        return result, [self.type_name(reader) for _ in range(count)]

    def value(self, reader, type_name):
        if type_name.endswith("[]"):
            count = reader.integer(4)
            return (
                None
                if count == 0xFFFFFFFF
                else [self.value(reader, type_name[:-2]) for _ in range(count)]
            )
        if type_name in ("string", "System.Type"):
            return reader.string()
        if type_name == "bool":
            return bool(reader.integer())
        sizes = {
            "byte": (1, False),
            "__int8": (1, True),
            "__int16": (2, True),
            "unsigned __int16": (2, False),
            "int": (4, True),
            "unsigned int": (4, False),
            "__int64": (8, True),
            "unsigned __int64": (8, False),
        }
        if type_name not in sizes:
            raise ValueError(f"不支持的特性参数类型：{type_name}")
        size, signed = sizes[type_name]
        return reader.integer(size, signed)

    def attrs(self, row, short_name):
        result = []
        for name, constructor, blob in self.attributes[id(row)]:
            if name.split("<", 1)[0].rsplit(".", 1)[-1].split("`")[0] != short_name:
                continue
            _, parameters = self.method_signature(Reader(constructor.Signature.value))
            reader = Reader(blob)
            if reader.take(2) != b"\x01\x00":
                raise ValueError("特性数据头无效")
            result.append(
                (name, [self.value(reader, parameter) for parameter in parameters])
            )
        return result

    def ignored(self, row):
        return bool(
            self.attrs(row, "CompilerGeneratedAttribute")
            or self.attrs(row, "CExporterIgnoreAttribute")
        )

    def field_type(self, row):
        reader = Reader(row.Signature.value)
        if reader.integer() != 0x06:
            raise ValueError("字段签名无效")
        return self.type_name(reader)


def native_name(name):
    name = name.replace("InteropGenerator.Runtime.CStringPointer", "byte*")
    name = name.replace("InteropGenerator.Runtime.Pointer", "Pointer")
    for prefix in (
        "FFXIVClientStructs.Expansion.FFXIV.",
        "FFXIVClientStructs.FFXIV.",
        "FFXIVClientStructs.STD.",
        "FFXIVClientStructs.Interop.",
    ):
        name = name.replace(prefix, "")
    name = re.sub(r"`\d+", "", name)
    name = name.replace("System.Numerics.", "")
    name = name.replace(".", "::")
    while re.search(r"Pointer<([^<>]+)>", name):
        name = re.sub(r"Pointer<([^<>]+)>", r"\1*", name)
    return name


def generic_parts(name):
    if "<" not in name or not name.endswith(">"):
        return None
    root, contents = name.split("<", 1)
    arguments = []
    depth = 0
    start = 0
    for index, character in enumerate(contents[:-1]):
        if character == "<":
            depth += 1
        elif character == ">":
            depth -= 1
        elif character == "," and depth == 0:
            arguments.append(contents[start:index].strip())
            start = index + 1
    arguments.append(contents[start:-1].strip())
    return root, arguments


def export_metadata(metadata):
    exported = {"enums": [], "structs": []}
    prefix = "FFXIVClientStructs.Expansion.FFXIV."
    for row in metadata.rows("TypeDef"):
        managed = metadata.row_name(row)
        if not managed.startswith(prefix) or metadata.ignored(row):
            continue
        if str(row.TypeName).endswith("VirtualTable") or str(row.TypeName) in (
            "Addresses",
            "MemberFunctionPointers",
            "StaticAddressPointers",
        ):
            continue
        if row.Extends.row is None:
            continue
        base_kind = metadata.token_name(row.Extends)
        if base_kind not in ("System.ValueType", "System.Enum"):
            continue
        name = native_name(managed)
        if base_kind == "System.Enum":
            values = {}
            underlying = None
            for item in row.FieldList:
                field = item.row
                if str(field.Name) == "value__":
                    underlying = metadata.field_type(field)
                elif id(field) in metadata.constants:
                    constant = metadata.constants[id(field)]
                    value = int.from_bytes(
                        constant.Value.value,
                        "little",
                        signed=constant.Type in (0x04, 0x06, 0x08, 0x0A),
                    )
                    values[str(field.Name)] = value
            exported["enums"].append(
                {
                    "type": name,
                    "name": name.rsplit("::", 1)[-1],
                    "namespace": "",
                    "underlying": underlying,
                    "flags": bool(metadata.attrs(row, "FlagsAttribute")),
                    "values": values,
                }
            )
            continue
        if not row.Flags.tdExplicitLayout:
            continue
        definition = {
            "type": name,
            "name": name.rsplit("::", 1)[-1],
            "namespace": "",
            "size": metadata.layouts.get(id(row)),
            "fields": [],
            "member_functions": [],
            "static_member_functions": [],
            "static_members": [],
            "virtual_functions": [],
            "union": False,
            "inherits": [],
            "comment": metadata.docs.get("T:" + managed, ""),
        }
        for inherited, args in metadata.attrs(row, "InheritsAttribute"):
            parent = inherited.split("<", 1)[1][:-1]
            definition["inherits"].append(
                {"type": native_name(parent), "offset": args[0]}
            )
        for item in row.FieldList:
            field = item.row
            if (
                field.Flags.fdStatic
                or metadata.ignored(field)
                or id(field) not in metadata.offsets
            ):
                continue
            field_name = str(field.Name)
            if field_name == "VirtualTable":
                continue
            raw_type = metadata.field_type(field)
            fixed = metadata.attrs(field, "FixedSizeArrayAttribute")
            buffer = metadata.attrs(field, "FixedBufferAttribute")
            record = {
                "name": field_name,
                "type": native_name(raw_type),
                "offset": metadata.offsets[id(field)],
                "base": False,
                "comment": metadata.docs.get("F:" + managed + "." + field_name, ""),
            }
            if fixed:
                array = re.fullmatch(r".*FixedSizeArray(\d+)`1<(.+)>", raw_type)
                if array is None:
                    raise ValueError(f"无法解析固定数组：{managed}.{field_name}")
                record.update(
                    type=native_name(array[2]),
                    size=int(array[1]),
                    is_string=fixed[0][1][0],
                )
                record["name"] = field_name.removesuffix("Storage")
            elif buffer:
                primitive = buffer[0][1][0].split(",", 1)[0]
                system_types = {
                    "System.Byte": "byte",
                    "System.SByte": "__int8",
                    "System.Int16": "__int16",
                    "System.UInt16": "unsigned __int16",
                    "System.Int32": "int",
                    "System.UInt32": "unsigned int",
                    "System.Int64": "__int64",
                    "System.UInt64": "unsigned __int64",
                    "System.Single": "float",
                    "System.Double": "double",
                    "System.Char": "wchar_t",
                }
                record.update(
                    type=system_types[primitive], size=buffer[0][1][1], is_string=False
                )
            elif " (*) (" in raw_type:
                return_type, arguments = raw_type.split(" (*) (", 1)
                parsed = generic_parts("Function<" + arguments[:-1] + ">")
                record.update(
                    type="__fastcall",
                    return_type=native_name(return_type),
                    parameters=[
                        {"name": f"a{i}", "type": native_name(argument)}
                        for i, argument in enumerate(parsed[1])
                        if argument
                    ],
                )
            bits = metadata.attrs(field, "BitFieldAttribute")
            if bits:
                record["bits"] = [
                    {
                        "type": native_name(attribute.split("<", 1)[1][:-1]),
                        "arguments": arguments,
                    }
                    for attribute, arguments in bits
                ]
            definition["fields"].append(record)
        for item in row.MethodList:
            method = item.row
            if metadata.ignored(method):
                continue
            member = metadata.attrs(method, "MemberFunctionAttribute")
            static = metadata.attrs(method, "StaticAddressAttribute")
            virtual = metadata.attrs(method, "VirtualFunctionAttribute")
            if not (member or static or virtual):
                continue
            result, parameters = metadata.method_signature(
                Reader(method.Signature.value)
            )
            names = {
                parameter.row.Sequence: str(parameter.row.Name)
                for parameter in method.ParamList
            }
            arguments = [
                {"name": names.get(i, f"a{i}"), "type": native_name(parameter)}
                for i, parameter in enumerate(parameters, 1)
            ]
            if not method.Flags.mdStatic:
                arguments.insert(0, {"name": "this", "type": name + "*"})
            record = {
                "name": str(method.Name),
                "return_type": native_name(result),
                "parameters": arguments,
            }
            if member:
                record["signature"] = member[0][1][0]
                definition[
                    "static_member_functions"
                    if method.Flags.mdStatic
                    else "member_functions"
                ].append(record)
            elif static:
                arguments = static[0][1]
                offsets = (
                    arguments[1] if isinstance(arguments[1], list) else [arguments[1]]
                )
                definition["static_members"].append(
                    {
                        "name": str(method.Name),
                        "signature": arguments[0],
                        "relative_follow_offsets": offsets,
                        "return_type": native_name(result).removesuffix("*"),
                        "is_pointer": arguments[2],
                    }
                )
            else:
                record["offset"] = virtual[0][1][0] * 8
                definition["virtual_functions"].append(record)
        vtable = metadata.attrs(row, "VirtualTableAttribute")
        if vtable:
            arguments = vtable[0][1]
            definition["vtable_signature"] = {
                "signature": arguments[0],
                "relative_follow_offsets": [arguments[1]],
            }
            definition["vtable_size"] = arguments[2] * 8
        exported["structs"].append(definition)
    return exported


def merge_structs(expansion, upstream):
    originals = {item["type"]: item for item in upstream["structs"]}
    structures = {}
    for definition in expansion["structs"]:
        name = definition["type"]
        overlay = any(
            parent["type"] == name and parent["offset"] == 0
            for parent in definition["inherits"]
        )
        if overlay:
            if name not in originals:
                raise ValueError(f"上游导出数据缺少被扩展的类型：{name}")
            merged = copy.deepcopy(originals[name])
            merged["size"] = max(definition["size"] or 0, merged.get("size") or 0)
            own_fields = definition["fields"]
            merged["fields"] = [
                field
                for field in merged["fields"]
                if all(
                    field["offset"] != own["offset"] and field["name"] != own["name"]
                    for own in own_fields
                )
            ] + own_fields
            for category in (
                "member_functions",
                "static_member_functions",
                "static_members",
                "virtual_functions",
            ):
                own = definition[category]
                merged[category] = [
                    item
                    for item in merged.get(category, [])
                    if all(item["name"] != replacement["name"] for replacement in own)
                ] + own
            merged.update(
                {
                    key: value
                    for key, value in definition.items()
                    if key in ("vtable_signature", "vtable_size", "comment") and value
                }
            )
        else:
            merged = copy.deepcopy(definition)
            for parent in definition["inherits"]:
                merged["fields"].append(
                    {
                        "name": parent["type"].rsplit("::", 1)[-1],
                        "type": parent["type"],
                        "offset": parent["offset"],
                        "base": True,
                    }
                )
        merged["fields"].sort(key=lambda field: field["offset"])
        structures[name] = merged
    enums = {item["type"]: item for item in upstream["enums"]}
    enums.update({item["type"]: item for item in expansion["enums"]})
    all_structs = {**originals, **structures}
    primitive_sizes = {
        "char": 1,
        "bool": 1,
        "byte": 1,
        "__int8": 1,
        "unsigned __int8": 1,
        "wchar_t": 2,
        "__int16": 2,
        "unsigned __int16": 2,
        "int": 4,
        "unsigned int": 4,
        "float": 4,
        "double": 8,
        "__int64": 8,
        "unsigned __int64": 8,
        "__fastcall": 8,
        "HANDLE": 8,
        "LPWIN32_FIND_DATAW": 8,
        "LPCRITICAL_SECTION": 8,
    }

    def measure(name, require_alignment=False):
        if name.endswith("*"):
            return 8, 8
        if name in primitive_sizes:
            size = primitive_sizes[name]
            return size, size
        if name in enums:
            return measure(enums[name]["underlying"], require_alignment)
        ensure_type(name)
        definition = all_structs[name]
        if not require_alignment and definition.get("size"):
            return definition["size"], None
        size = 0
        alignment = 1
        for member in definition["fields"]:
            width, boundary = measure(member["type"], True)
            alignment = max(alignment, min(boundary, 8))
            size = max(size, member["offset"] + width * member.get("size", 1))
        size = max(size, definition.get("size") or 0)
        return (size + alignment - 1) // alignment * alignment, alignment

    def ensure_type(name):
        if (
            name in all_structs
            or name in enums
            or name in primitive_sizes
            or name == "void"
        ):
            return
        if name == "Vector3":
            all_structs[name] = {
                "type": name,
                "name": name,
                "namespace": "",
                "size": 12,
                "union": False,
                "fields": [
                    {"type": "float", "name": axis, "offset": offset * 4}
                    for offset, axis in enumerate("XYZ")
                ],
                "member_functions": [],
            }
            return
        generic = generic_parts(name)
        if generic is None:
            return
        candidates = []
        for key, prototype in originals.items():
            parsed = generic_parts(key)
            if (
                parsed
                and parsed[0] == generic[0]
                and len(parsed[1]) == len(generic[1])
                and len(set(parsed[1])) == len(parsed[1])
            ):
                candidates.append((prototype, parsed[1]))
        if not candidates:
            raise ValueError(f"缺少泛型布局模板：{name}")
        prototype, parameters = candidates[0]
        replacements = dict(zip(parameters, generic[1]))
        pattern = re.compile(
            "|".join(
                re.escape(parameter)
                for parameter in sorted(parameters, key=len, reverse=True)
            )
        )
        specialized = copy.deepcopy(prototype)
        specialized.update(type=name, name=name, fields=[], member_functions=[])
        specialized.pop("size", None)
        all_structs[name] = specialized
        offset = 0
        alignment = 1
        for member in prototype["fields"]:
            field = copy.deepcopy(member)
            field["type"] = pattern.sub(
                lambda match: replacements[match[0]], field["type"]
            )
            width, boundary = measure(field["type"], True)
            boundary = min(boundary, 8)
            offset = (offset + boundary - 1) // boundary * boundary
            field["offset"] = offset
            offset += width * field.get("size", 1)
            alignment = max(alignment, boundary)
            specialized["fields"].append(field)
        specialized["size"] = (offset + alignment - 1) // alignment * alignment

    for name in structures:
        definition = all_structs[name]
        fields = definition["fields"]
        while True:
            expanded = False
            for index, field in enumerate(fields):
                if not field.get("base") or field["type"] not in all_structs:
                    continue
                start = field["offset"]
                end = start + measure(field["type"])[0]
                if not any(
                    other is not field
                    and other["offset"] < end
                    and start
                    < other["offset"] + measure(other["type"])[0] * other.get("size", 1)
                    for other in fields
                ):
                    continue
                parent = all_structs[field["type"]]
                replacements = copy.deepcopy(parent["fields"])
                for replacement in replacements:
                    replacement["offset"] += start
                fields = fields[:index] + replacements + fields[index + 1 :]
                virtuals = {
                    function["offset"]: copy.deepcopy(function)
                    for function in parent.get("virtual_functions", [])
                }
                virtuals.update(
                    {
                        function["offset"]: function
                        for function in definition.get("virtual_functions", [])
                    }
                )
                if virtuals:
                    definition["virtual_functions"] = [
                        virtuals[offset] for offset in sorted(virtuals)
                    ]
                expanded = True
                break
            if not expanded:
                break
        unique = {}
        for field in fields:
            unique[(field["name"], field["offset"], field["type"])] = field
        fields = sorted(unique.values(), key=lambda field: field["offset"])
        groups = []
        end = -1
        for field in fields:
            field_end = field["offset"] + measure(field["type"])[0] * field.get(
                "size", 1
            )
            if field["offset"] >= end:
                groups.append([])
            groups[-1].append(field)
            end = max(end, field_end)
        definition["fields"] = []
        for group in groups:
            if len(group) == 1 or definition.get("union"):
                definition["fields"].extend(group)
                continue
            start = group[0]["offset"]
            end = max(
                field["offset"] + measure(field["type"])[0] * field.get("size", 1)
                for field in group
            )
            union_name = name + "::Layout" + str(start)
            union = {
                "type": union_name,
                "name": "Layout" + str(start),
                "namespace": "",
                "union": True,
                "size": end - start,
                "fields": [],
                "member_functions": [],
            }
            for index, field in enumerate(group):
                member = copy.deepcopy(field)
                member["base"] = False
                member["offset"] -= start
                if member["offset"]:
                    view_name = union_name + "::View" + str(index)
                    all_structs[view_name] = {
                        "type": view_name,
                        "name": "View" + str(index),
                        "namespace": "",
                        "union": False,
                        "size": end - start,
                        "fields": [member],
                        "member_functions": [],
                    }
                    member = {
                        "name": member["name"] + "View",
                        "type": view_name,
                        "offset": 0,
                    }
                union["fields"].append(member)
            all_structs[union_name] = union
            definition["fields"].append(
                {"name": "Layout" + str(start), "type": union_name, "offset": start}
            )
    required_structs = set(structures)
    required_enums = {item["type"] for item in expansion["enums"]}
    pending = list(structures)
    while pending:
        definition = all_structs[pending.pop()]
        texts = [field["type"] for field in definition["fields"]]
        for field in definition["fields"]:
            if "return_type" in field:
                texts.append(field["return_type"])
                texts.extend(parameter["type"] for parameter in field["parameters"])
        for category in (
            "member_functions",
            "static_member_functions",
            "virtual_functions",
        ):
            for function in definition.get(category, []):
                if "return_type" in function:
                    texts.append(function["return_type"])
                texts.extend(
                    parameter["type"] for parameter in function.get("parameters", [])
                )
        texts.extend(
            member["return_type"] for member in definition.get("static_members", [])
        )
        for type_name in texts:
            referenced = type_name.rstrip("*")
            ensure_type(referenced)
            if referenced in all_structs and referenced not in required_structs:
                required_structs.add(referenced)
                pending.append(referenced)
            if referenced in enums:
                required_enums.add(referenced)
    ordered = []
    visited = set()
    visiting = set()

    def visit(name):
        if name in visited:
            return
        if name in visiting:
            raise ValueError(f"结构存在值类型循环依赖：{name}")
        visiting.add(name)
        definition = all_structs[name]
        for field in definition["fields"]:
            if field["type"] in required_structs:
                visit(field["type"])
        visiting.remove(name)
        visited.add(name)
        if not definition.get("virtual_functions"):
            definition.pop("virtual_functions", None)
        ordered.append(definition)

    for name in sorted(required_structs):
        visit(name)
    return {
        "enums": [enums[name] for name in sorted(required_enums)],
        "structs": ordered,
        "expansion_types": sorted(structures),
        "generated_types": sorted(
            required_structs - originals.keys() - structures.keys()
        ),
        "expansion_enums": [item["type"] for item in expansion["enums"]],
    }


def main():
    parser = argparse.ArgumentParser(
        description="从扩展程序集元数据生成 IDA 类型与函数声明"
    )
    parser.add_argument("--assembly", required=True, type=Path)
    parser.add_argument("--ffcs-ida", required=True, type=Path)
    parser.add_argument("--client", required=True, type=Path)
    parser.add_argument("--output", required=True, type=Path)
    args = parser.parse_args()
    if args.output.resolve() in {
        args.assembly.resolve(),
        args.client.resolve(),
        args.ffcs_ida.resolve() / "data.yml",
        args.ffcs_ida.resolve() / "ffxiv_structs.yml",
    }:
        parser.error("输出路径不得覆盖程序集、客户端或上游数据")
    sys.path.insert(0, str(args.ffcs_ida.resolve()))
    from ffxiv_crossclient import Image, resolve_symbols

    metadata = Metadata(args.assembly)
    expansion = export_metadata(metadata)
    upstream = yaml.load(
        (args.ffcs_ida / "ffxiv_structs.yml").read_text(encoding="utf-8"),
        Loader=yaml.CSafeLoader,
    )
    payload = merge_structs(expansion, upstream)
    payload.update(resolve_symbols(expansion["structs"], Image(args.client)))
    payload.update(
        schema=1,
        assembly_sha256=hashlib.sha256(args.assembly.read_bytes()).hexdigest(),
        ffcs_ida=str(args.ffcs_ida.resolve()),
    )
    with args.output.open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(payload, stream, ensure_ascii=False, indent=2)
        stream.write("\n")
    print(
        json.dumps(
            {
                "expansion_structs": len(expansion["structs"]),
                "expansion_enums": len(expansion["enums"]),
                "required_structs": len(payload["structs"]),
                "required_enums": len(payload["enums"]),
                "resolved_signatures": len(payload["resolved_signatures"]),
                "unresolved": len(payload["unresolved"]),
            },
            ensure_ascii=False,
        )
    )


if __name__ == "__main__":
    main()
