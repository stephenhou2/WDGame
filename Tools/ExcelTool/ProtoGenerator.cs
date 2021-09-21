using System.Text;
using System.IO;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace ExcelTool
{
    public class ProtoGenerator
    {
        public ProtoGenerator()
        {
        }

        public void ExportProto(ExcelSheet sheet)
        {
            string tableName = sheet.SheetName;
            StringBuilder protoString = new StringBuilder();
            protoString.Append("syntax =  \"proto3\";\r\npackage TableProto;\r\n");

            protoString.AppendFormat("\r\n\r\nmessage {0}\r\n", tableName);
            protoString.Append("{\r\n");

            for (int i = 0; i < sheet.DataTypes.Count; i++)
            {
                DataType type = sheet.DataTypes[i];
                string key = sheet.DataKeys[i];
                int index = i + 1;
                if (type == DataType.UInt)
                {
                    protoString.AppendFormat("  uint32 {0} = {1};\r\n", key, index);
                }
                if (type == DataType.Int)
                {
                    protoString.AppendFormat("  int32 {0} = {1};\r\n", key, index);
                }
                else if (type == DataType.String)
                {
                    protoString.AppendFormat("  string {0} = {1};\r\n", key, index);
                }
                else if (type == DataType.Array)
                {
                    protoString.AppendFormat("  repeated int32 {0} = {1};\r\n", key, index);
                }
            }

            protoString.Append("}");

            protoString.AppendFormat("\r\n\r\nmessage TB_{0}\r\n", tableName);
            protoString.AppendFormat("{{\r\n  repeated {0} data = 1;\r\n}}", tableName);
            string protoPath = Path.Combine(Define.ProtoDir,string.Format("{0}.proto",tableName));
            File.WriteAllText(protoPath, protoString.ToString(), Encoding.UTF8);
        }

        public void ExportCSharp(ExcelSheet sheet)
        {
            StringBuilder str = new StringBuilder();
            str.Append("using NPOI.SS.UserModel;\r\n");
            str.AppendFormat("public class {0}Export", sheet.SheetName);

            str.Append(@"
{
    public void Export(ExcelReader reader)
    {");
            str.AppendFormat("\r\n\t\tISheet sheetData = reader.GetSheet(\"{0}\");",sheet.SheetName);

            str.Append(@"
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            return;
        }
        string sheetName = sheet.SheetName;
");
            str.AppendFormat("\t\tTableProto.TB_{0} v = new TableProto.TB_{0}();\r\n", sheet.SheetName);
            str.Append(@"         for (int i = 0; i < sheet.DataRowCount; i++)
         {
");
            str.AppendFormat("\t\t\tTableProto.{0} cfg = new TableProto.{0}();\r\n", sheet.SheetName);

            str.Append(@"             for (int j = 0; j < sheet.DataColCnt; j++)
            {
                var field = sheet.DataValues[i][j];
");
            for (int i = 0; i < sheet.DataTypes.Count; i++)
            {
                DataType type = sheet.DataTypes[i];
                string key = sheet.DataKeys[i];

                if (type == DataType.UInt)
                {
                    str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetUIntFieldValue(field);\r\n", key);
                }
                else if (type == DataType.Int)
                {
                    str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetIntFieldValue(field);\r\n", key);
                }
                else if (type == DataType.String)
                {
                    str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetStringFieldValue(field);\r\n", key);
                }
                else if (type == DataType.Array)
                {

                    str.Append(@"               var t = ProtoDataExpoter.GetArrayFieldValue(field); 
                for(int m = 0;m<t.Length;m++)
                {");
                    str.AppendFormat("\r\n\t\t\t\t\tcfg.{0}.Add(t[m]);\r\n",key);
                    str.Append("\r\n\t\t\t\t}");
                }
            }

            str.Append(@"
            }
        v.Data.Add(cfg);
         ProtoDataHandler.SaveProtoData(v,Define.ProtoDir+'/'+sheetName+"".bin"");
        }
    }
}");
            string protoPath = Path.Combine(Define.ExporterDir, string.Format("{0}Export.cs", sheet.SheetName));
            File.WriteAllText(protoPath, str.ToString(), Encoding.UTF8);
        }

        public void ExportRegister(ExcelReader reader)
        {
            var allSheets = reader.GetAllSheets();
            StringBuilder str = new StringBuilder();

            str.Append(@"public class ExcelExportRegister
{
    public void ExportAllProtoData()
    {
        ExcelReader reader = new ExcelReader();
        int readRet = reader.ReadAllTableExcel();
        if(readRet == 0)
        {
");
            int cnt = 0;
            foreach(KeyValuePair<string,ISheet> kv in allSheets)
            {
                var sheet = kv.Value;
                str.AppendFormat("\t\t\t{0}Export v{1} = new {0}Export();\r\n",sheet.SheetName, cnt);
                str.AppendFormat("\t\t\tv{0}.Export(reader);\r\n",cnt);
                cnt++;
            }
            str.Append(@"
        }
    }
}");

            string protoPath = Path.Combine(Define.ExporterDir, string.Format("ExcelExportRegister.cs"));
            File.WriteAllText(protoPath, str.ToString(), Encoding.UTF8);
        }

        public void ExportTableReader(ExcelReader reader)
        {

        }
    }
}


