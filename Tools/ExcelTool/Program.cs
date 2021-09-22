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
                Console.ReadKey();
                return;
            }

            var sheets = reader.GetAllSheets();
            foreach (KeyValuePair<string,ISheet> kv in sheets)
            {
                ExcelSheet es = new ExcelSheet();
                var sheet = kv.Value;

                Dictionary<int,FieldInfo> fieldInfos = es.ReadTableFieldDefineRow(sheet);
                if (fieldInfos == null)
                {
                    Console.ReadKey();
                    return;
                }

                protoGen.ExportProto(sheet.SheetName,fieldInfos);

                protoGen.ExportCSharp(sheet.SheetName,fieldInfos);
            }

            protoGen.ExportRegister(reader);

            Console.ReadKey();
        }
    }
}
