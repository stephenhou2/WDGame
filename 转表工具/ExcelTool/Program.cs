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

            ExcelReader reader = new ExcelReader();
            int readRet = reader.ReadAllTableExcel();
            if(readRet < 0)
            {
                Console.WriteLine("<color=red>Read Excel Table Failed!</color>");
                return;
            }

            var sheets = reader.GetSheetList();
            foreach (var sheet in sheets)
            {
                ExcelSheet es = new ExcelSheet();
                es.ReadExcelData(sheet);
                protoGen.AppendProto(es);
            }

            protoGen.ExportProto();

            Console.ReadLine();
        }
    }
}
