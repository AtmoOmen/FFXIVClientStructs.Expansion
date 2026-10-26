# 扩展项目 IDA 导入

`ffxiv_expansion_export.py` 直接读取编译后程序集的元数据，导出结构、枚举、字段偏移、继承、固定数组、位域说明与原生函数声明，并在指定客户端中解析地址。不需要维护扩展项目的人工地址表。

需要 Python 3.10 或更高版本，以及 `dnfile`、`PyYAML`、`pefile`、`capstone`。本目录通过 FFCS 新增的 `ffxiv_crossclient.py` 复用客户端解析能力，通过 `ffxiv_crossclient_ida.py` 复用导入入口。

## 导出

先正常构建扩展项目。若需要一并导入中文 XML Doc，可以在构建解决方案时指定 `-p:GenerateDocumentationFile=true`；导出器会读取程序集旁的同名 XML 文件。导出器不会加载或执行扩展程序集中的原生互操作代码。

在当前 `ida` 目录执行：

```powershell
$ffcsIda = 'D:\Personal\Document\GitHub\Dalamud\lib\FFXIVClientStructs\ida'
$assembly = '..\FFXIVClientStructs.Expansion\bin\Release\FFXIVClientStructs.Expansion.dll'
$client = 'E:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\clients\7.57hf1\ffxiv_dx11.exe'

python -X utf8 -B .\ffxiv_expansion_export.py --assembly $assembly --ffcs-ida $ffcsIda --client $client --output .\expansion.generated.json
```

国服使用相同命令，将 `--client` 改为国服程序，并指定另一份输出文件即可。

导出文件保留程序集与客户端的 SHA-256、已解析签名以及未解析声明。未解析声明保留类型信息，但不会凭借不确定匹配应用地址。使用另一个客户端或重新构建后的程序集时，应重新导出。

## 在 IDA 中导入

先完成 FFCS 基础类型的导入，再运行本目录的 `ffxiv_expansion_ida.py`，选择刚生成的 JSON。JSON 记录导出时指定的 FFCS `ida` 路径，因此不需要将上游脚本复制到扩展项目。若移动了 FFCS 目录，应使用新路径重新导出。

入口会更新扩展类型并导入缺失依赖，已有基础依赖类型直接复用。位域信息与可用的 XML Doc 会写入成员说明。

导入中断后可以重新运行入口并选择同一份 JSON。更换客户端或重新构建程序集后，应先重新导出。
