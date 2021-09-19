using System;

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
                string protoStr = protoGen.GetProtoString(es);
                if (!string.IsNullOrEmpty(protoStr))
                {
                    protoGen.ExportProto(protoStr,es.SheetName);
                }
            }

            Console.ReadLine();
        }
    }
}
