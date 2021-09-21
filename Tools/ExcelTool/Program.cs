using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace ExcelTool
{
    class Program
    {
        static void Main()
        {
            ProtoGenerator protoGen = new ProtoGenerator();

            ExcelReader reader = new ExcelReader();
            int readRet = reader.ReadAllTableExcel();
            if(readRet < 0)
            {
                Console.WriteLine("<color=red>Read Excel Table Failed!</color>");
                return;
            }

            var sheets = reader.GetAllSheets();
            protoGen.ExportRegister(reader);
            foreach (KeyValuePair<string,ISheet> kv in sheets)
            {
                ExcelSheet es = new ExcelSheet();
                var sheet = kv.Value;
                es.ReadExcelFields(sheet);

                protoGen.ExportProto(es);
                protoGen.ExportCSharp(es);
            }
        }
    }
}
