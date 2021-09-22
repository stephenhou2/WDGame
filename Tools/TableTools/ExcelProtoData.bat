@echo off

ExcelTool.exe

for %%i in (./output/proto/*.proto) do (
protoc --csharp_out=./output/protoCs ./output/proto/%%i
echo %%i生成成功
)

for %%i in (./output/protoCs/*.cs) do (
copy .\output\protoCs\%%i ..\ExcelProtoDataTool\ProtoCS
)

for %%i in (./output/exporter/*.cs) do (
copy .\output\exporter\%%i ..\ExcelProtoDataTool\ProtoExport
)


msbuild ..\ExcelProtoDataTool/ExcelProtoDataTool.csproj 

ExcelProtoDataTool.exe

pause