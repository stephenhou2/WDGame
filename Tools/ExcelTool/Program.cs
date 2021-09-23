using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace ExcelTool
{
    class Program
    {
        static int Main()
        {
            ProtoGenerator protoGen = new ProtoGenerator();

            ExcelReader reader = new ExcelReader();
            int readRet = reader.ReadAllTableExcel();
            if(readRet < 0)
            {
                Console.WriteLine("<color=red>Read Excel Table Failed!</color>");
                //Console.ReadKey();
                return -1;
            }

            var sheets = reader.GetAllSheets();
            foreach (KeyValuePair<string,ISheet> kv in sheets)
            {
                ExcelSheet es = new ExcelSheet();
                var sheet = kv.Value;

                int ret  = es.ReadTableFieldDefineRow(sheet);
                if (ret < 0)
                {
                    //Console.ReadKey();
                    return -2;
                }

                List<FieldInfo> fieldInfos = es.FieldInfos;
                protoGen.ExportProto(sheet.SheetName,fieldInfos);

                protoGen.ExportCSharp(sheet.SheetName,fieldInfos);
            }

            protoGen.ExportRegister(reader);

            //Console.ReadKey();
            return 0;
        }
    }
}
