@echo off

protoc --csharp_out=./output ./GameMapData.proto
echo proto文件生成成功

move .\output\GameMapData.cs ..\Assets\Scripts\Proto

pause