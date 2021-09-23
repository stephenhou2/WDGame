@echo off

echo Reading Tables...
echo ----------------------------------------
ExcelTool.exe
echo.

if %ERRORLEVEL% NEQ 0 (
	echo ExcelTool Excute Failed...
	pause
	EXIT /B 0
)



echo generate Proto csharp files...
echo ----------------------------------------
for %%i in (./output/proto/*.proto) do (
	protoc --csharp_out=./output/protoCs ./output/proto/%%i
	echo %%i生成成功
)
echo.

echo copy proto csharp fiels...
echo ----------------------------------------
for %%i in (./output/protoCs/*.cs) do (
	copy .\output\protoCs\%%i ..\ExcelProtoDataTool\ProtoCS
)
echo.


echo copy pdData exporter files fiels to ExelProtoDataTool...
echo ----------------------------------------
for %%i in (./output/exporter/*.cs) do (
	copy .\output\exporter\%%i ..\ExcelProtoDataTool\ProtoExport
)
echo.

echo start build ExcelProtoDataTool...
echo ----------------------------------------
msbuild ..\ExcelProtoDataTool/ExcelProtoDataTool.csproj 

if %ERRORLEVEL% NEQ 0 (
	echo build ExcelProtoDataTool Failed...
	pause
	EXIT /B 0
)

echo build ExcelProtoDataTool finish...
echo.


echo start export pb bytes...
echo ----------------------------------------
ExcelProtoDataTool.exe
echo export pb bytes finish
pause