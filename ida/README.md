
# 脚本说明

## 导入至 IDA

### 运行要求

- Python 3.10 及更高版本，以及 `dnfile`、`PyYAML`、`pefile`、`capstone`。
- 国服 FFXIVClientStructs 项目（下称：FFCS）独有的 `ffxiv_crossclient.py` 和 `ffxiv_crossclient_ida.py` 脚本。
- IDA 9 及更高版本。
- Powershell 7 及更高版本。

### 运行过程

1. 完成 FFCS 项目的 IDA 导入。
2. 构建本项目。推荐指定 `-p:GenerateDocumentationFile=true`。导出脚本会一并读取程序集旁同名 XML 文件，并在后续处理为 IDA 中的注释。
3. 在当前目录执行下面的 Powershell 指令：

```powershell
$ffcsIDA = 'D:\Personal\Document\GitHub\Dalamud\lib\FFXIVClientStructs\ida'
$assembly = 'D:\Personal\Document\GitHub\DailyRoutines\bin\Debug\FFXIVClientStructs.Expansion.dll'
$client = 'E:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\clients\7.58\ffxiv_dx11.exe'

python -X utf8 -B .\ffxiv_expansion_export.py --assembly $assembly --ffcs-ida $ffcsIDA --client $client --output .\expansion.generated.json
```

将 `ffcsIDA` 路径替换为本机环境下的国服 FFCS 相关路径；将 `client` 路径替换为本机环境下的目标导入客户端，不局限于特定服务器客户端。

在当前目录下，将输出包含符号信息的 `expansion.generated.json` 文件。其中包含指定的 FFCS 路径，因此在此期间如果移动了 FFCS 目录，应使用新路径重新导出。

4. 在 IDA 中选择运行本目录的 `ffxiv_expansion_ida.py` 脚本，在弹出的文件选择对话框中选择上一步生成的 `expansion.generated.json` 文件。等待脚本运行完成即可。