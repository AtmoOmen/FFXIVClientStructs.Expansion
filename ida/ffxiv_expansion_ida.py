from __future__ import annotations

import importlib.util
import json
from pathlib import Path


def run():
    import ida_kernwin

    selected = ida_kernwin.ask_file(False, "*.json", "选择为当前客户端导出的扩展数据")
    if not selected:
        return
    payload = json.loads(Path(selected).read_text(encoding="utf-8"))
    entry = Path(payload["ffcs_ida"]) / "ffxiv_crossclient_ida.py"
    spec = importlib.util.spec_from_file_location("ffxiv_crossclient_ida", entry)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    module.run(expansion_path=selected)


if __name__ == "__main__":
    run()
