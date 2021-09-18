@echo off

for %%i in (./output/*.proto) do (
protoc --csharp_out=./csharp_output ./output/%%i
echo %%i生成成功
)

for %%i in (./csharp_output/*.cs) do (
copy .\csharp_output\%%i .\ExcelProtoDataTool
)

pause