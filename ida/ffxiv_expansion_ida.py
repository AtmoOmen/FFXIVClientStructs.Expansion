from __future__ import annotations

import importlib.util
import json
import sys
from pathlib import Path


def run():
    import ida_kernwin

    selected = ida_kernwin.ask_file(False, "*.json", "选择为当前客户端导出的扩展数据")
    if not selected:
        return
    payload = json.loads(Path(selected).read_text(encoding="utf-8"))
    directory = Path(payload["ffcs_ida"])
    schema_spec = importlib.util.spec_from_file_location(
        "structs_schema", directory / "structs_schema.py"
    )
    schema = importlib.util.module_from_spec(schema_spec)
    schema_spec.loader.exec_module(schema)
    sys.modules["structs_schema"] = schema
    entry = directory / "ffxiv_crossclient_ida.py"
    spec = importlib.util.spec_from_file_location("ffxiv_crossclient_ida", entry)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    module.run(expansion_path=selected)


if __name__ == "__main__":
    run()
