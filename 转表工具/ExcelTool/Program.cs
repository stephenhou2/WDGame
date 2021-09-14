using System;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExcelTool
{
    class Program
    {
        static void Main(string[] args)
        {
            ProtoGenerator protoGen = new ProtoGenerator();

            string path = "E:/Learning/Unity/MapEditor/转表工具/Test.xlsx";

            List<ISheet> sheets = ExcelReader.ReadExcel(path);
            foreach(var sheet in sheets)
            {
                ExcelSheet es = new ExcelSheet();
                es.ReadExcelData(sheet);
                protoGen.AppedProto(es);
            }

            protoGen.ExportProto("E:/Learning/Unity/MapEditor/转表工具/TableData.proto");

            Process proc = new Process();
            string targetDir = string.Format("E:/Learning/Unity/MapEditor/转表工具");

            proc.StartInfo.WorkingDirectory = targetDir;
            proc.StartInfo.FileName = "ExcelData.bat";

            proc.Start();
            proc.WaitForExit();

            Console.ReadLine();
        }
    }
}
