@echo off

protoc --csharp_out=./output ./output/TableData.proto
echo proto文件生成成功

copy .\output\TableData.cs ..\Assets\Scripts\Proto