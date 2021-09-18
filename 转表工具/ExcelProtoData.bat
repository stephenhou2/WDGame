@echo off

for %%i in (./output/*.proto) do (
protoc --csharp_out=./output ./output/%%i
)

::protoc --csharp_out=./output ./output/TableData.proto
echo proto文件生成成功

copy .\output\TableData.cs ..\Assets\Scripts\Proto
pause