@echo off

protoc --csharp_out=./output ./MapData.proto
echo proto文件生成成功

copy .\output\MapData.cs ..\Assets\Scripts\Proto

pause