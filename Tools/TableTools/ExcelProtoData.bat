@echo off

echo 删除output中的原有脚本和pb数据...
echo ----------------------------------------
echo.
del .\output\datatable_cs\ /F /Q
del .\output\exporter\ /F /Q
del .\output\pbbytes\ /F /Q
del .\output\proto\ /F /Q
del .\output\protoCs\ /F /Q

echo 开始读取表格数据...
echo ----------------------------------------
ExcelTool.exe
echo.

if %ERRORLEVEL% NEQ 0 (
	echo ExcelTool Excute Failed...
	pause
	EXIT /B 0
)



echo 开始生成proto csharp定义文件...
echo ----------------------------------------
for %%i in (./output/proto/*.proto) do (
	protoc --csharp_out=./output/protoCs ./output/proto/%%i
	echo %%i生成成功
)
echo.

echo 复制proto csharp定义文件到ExcelProtoDataTool...
echo ----------------------------------------
copy .\output\protoCs\*.cs ..\ExcelProtoDataTool\ProtoCS /Y
echo.


echo 复制proto 导出用文件到ExcelProtoDataTool...
echo ----------------------------------------
copy .\output\exporter\*.cs ..\ExcelProtoDataTool\ProtoExport /Y
echo.

echo 开始构建 ExcelProtoDataTool...
echo ----------------------------------------
msbuild ..\ExcelProtoDataTool/ExcelProtoDataTool.csproj 

if %ERRORLEVEL% NEQ 0 (
	echo ExcelProtoDataTool 构建失败...
	pause
	EXIT /B 0
)

echo ExcelProtoDataTool 构建完成...
echo.


echo 开始导出pb数据...
echo ----------------------------------------
ExcelProtoDataTool.exe

if %ERRORLEVEL% NEQ 0 (
	echo pb数据导出失败...
	pause
	EXIT /B 0
)

echo pb数据导出完成

echo 开始复制到Unity工程...
echo ----------------------------------------
echo.

echo 删除原有proto定义和加载文件
del ..\..\WDGame\Assets\Scripts\Proto\ProtoDef\ /F /Q
del ..\..\WDGame\Assets\Scripts\Table\ /F /Q
del ..\..\WDGame\Data\TableData\ /F /Q
echo ----------------------------------------
echo.

echo 复制新的proto定义和加载文件
copy .\output\protoCs\*.cs ..\..\WDGame\Assets\Scripts\Proto\ProtoDef /Y
copy .\output\datatable_cs\*.cs ..\..\WDGame\Assets\Scripts\Table /Y
copy .\output\pbbytes\*.bin ..\..\WDGame\Data\TableData /Y

pause