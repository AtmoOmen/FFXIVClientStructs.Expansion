from __future__ import annotations

import unittest

from ffxiv_expansion_export import Reader, generic_parts, merge_structs, native_name


class MetadataTests(unittest.TestCase):
    def test_compressed_integer_encodings(self):
        self.assertEqual(Reader(bytes([0x7F])).compressed(), 0x7F)
        self.assertEqual(Reader(bytes([0x80, 0x80])).compressed(), 0x80)
        self.assertEqual(Reader(bytes([0xC0, 0x01, 0x00, 0x00])).compressed(), 0x10000)

    def test_attribute_string_uses_utf8(self):
        value = "原生字段".encode()
        self.assertEqual(Reader(bytes([len(value)]) + value).string(), "原生字段")

    def test_nested_generic_arguments(self):
        self.assertEqual(
            generic_parts("StdMap<unsigned int, StdVector<Example*>>"),
            ("StdMap", ["unsigned int", "StdVector<Example*>"]),
        )

    def test_native_type_names_preserve_pointer_depth(self):
        self.assertEqual(
            native_name(
                "InteropGenerator.Runtime.Pointer`1<FFXIVClientStructs.Expansion.FFXIV.Client.Game.Example>"
            ),
            "Client::Game::Example*",
        )
        self.assertEqual(
            native_name("InteropGenerator.Runtime.CStringPointer"), "byte*"
        )

    def test_overlay_keeps_base_fields_and_expands_to_effective_size(self):
        upstream = {
            "enums": [],
            "structs": [
                {
                    "type": "Example",
                    "name": "Example",
                    "size": 16,
                    "fields": [
                        {"name": "Base", "type": "int", "offset": 0},
                        {"name": "Unknown", "type": "int", "offset": 8},
                    ],
                    "member_functions": [],
                }
            ],
        }
        expansion = {
            "enums": [],
            "structs": [
                {
                    "type": "Example",
                    "name": "Example",
                    "size": 12,
                    "fields": [{"name": "Known", "type": "int", "offset": 8}],
                    "inherits": [{"type": "Example", "offset": 0}],
                    "member_functions": [],
                    "static_member_functions": [],
                    "static_members": [],
                    "virtual_functions": [],
                    "comment": "",
                }
            ],
        }
        result = merge_structs(expansion, upstream)
        self.assertEqual(result["structs"][0]["size"], 16)
        self.assertEqual(
            [field["name"] for field in result["structs"][0]["fields"]],
            ["Base", "Known"],
        )
        self.assertEqual(upstream["structs"][0]["fields"][1]["name"], "Unknown")

    def test_vector_specialization_uses_existing_layout(self):
        upstream = {
            "enums": [],
            "structs": [
                {
                    "type": "StdVector<int>",
                    "name": "StdVector",
                    "fields": [
                        {"name": "First", "type": "int*", "offset": 0},
                        {"name": "Last", "type": "int*", "offset": 8},
                        {"name": "End", "type": "int*", "offset": 16},
                    ],
                    "member_functions": [],
                }
            ],
        }
        expansion = {
            "enums": [],
            "structs": [
                {
                    "type": "Example",
                    "name": "Example",
                    "size": 24,
                    "fields": [
                        {
                            "name": "Values",
                            "type": "StdVector<unsigned int>",
                            "offset": 0,
                        }
                    ],
                    "inherits": [],
                    "member_functions": [],
                    "static_member_functions": [],
                    "static_members": [],
                    "virtual_functions": [],
                    "comment": "",
                }
            ],
        }
        result = merge_structs(expansion, upstream)
        vector = next(
            definition
            for definition in result["structs"]
            if definition["type"] == "StdVector<unsigned int>"
        )
        self.assertEqual(vector["size"], 24)
        self.assertEqual([field["offset"] for field in vector["fields"]], [0, 8, 16])
        self.assertEqual(vector["fields"][0]["type"], "unsigned int*")

    def test_overlapping_fields_become_union_views(self):
        upstream = {"enums": [], "structs": []}
        expansion = {
            "enums": [],
            "structs": [
                {
                    "type": "Example",
                    "name": "Example",
                    "size": 4,
                    "fields": [
                        {"name": "Flags", "type": "unsigned int", "offset": 0},
                        {"name": "FlagByte", "type": "byte", "offset": 1},
                    ],
                    "inherits": [],
                    "member_functions": [],
                    "static_member_functions": [],
                    "static_members": [],
                    "virtual_functions": [],
                    "comment": "",
                }
            ],
        }
        result = merge_structs(expansion, upstream)
        definitions = {item["type"]: item for item in result["structs"]}
        union = definitions["Example::Layout0"]
        self.assertTrue(union["union"])
        self.assertEqual(union["size"], 4)
        self.assertEqual(
            definitions["Example::Layout0::View1"]["fields"][0]["offset"], 1
        )
        self.assertNotIn("virtual_functions", definitions["Example"])
        order = [item["type"] for item in result["structs"]]
        self.assertLess(
            order.index("Example::Layout0::View1"), order.index("Example::Layout0")
        )
        self.assertLess(order.index("Example::Layout0"), order.index("Example"))

    def test_base_layout_is_expanded_when_extension_uses_its_padding(self):
        upstream = {
            "enums": [],
            "structs": [
                {
                    "type": "Base",
                    "name": "Base",
                    "size": 16,
                    "fields": [
                        {"name": "First", "type": "int", "offset": 0},
                        {"name": "Last", "type": "int", "offset": 12},
                    ],
                    "member_functions": [],
                }
            ],
        }
        expansion = {
            "enums": [],
            "structs": [
                {
                    "type": "Example",
                    "name": "Example",
                    "size": 16,
                    "fields": [{"name": "New", "type": "int", "offset": 8}],
                    "inherits": [{"type": "Base", "offset": 0}],
                    "member_functions": [],
                    "static_member_functions": [],
                    "static_members": [],
                    "virtual_functions": [],
                    "comment": "",
                }
            ],
        }
        result = merge_structs(expansion, upstream)
        definition = next(
            item for item in result["structs"] if item["type"] == "Example"
        )
        self.assertEqual(
            [(field["name"], field["offset"]) for field in definition["fields"]],
            [("First", 0), ("New", 8), ("Last", 12)],
        )


if __name__ == "__main__":
    unittest.main()
