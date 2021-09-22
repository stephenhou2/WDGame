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

        const string PBStr = @"syntax =  ""proto3"";
package TableProto;

message {0}
{{
{1}
}}

message TB_{0}
{{
    repeated {0} data = 1;
}}
";

        private void AppendPbDef(StringBuilder str,DataType type,string key, int index)
        {
            if (type == DataType.UInt)
            {
                str.AppendFormat("\tuint32 {0} = {1};\r\n", key, index);
            }
            if (type == DataType.Int)
            {
                str.AppendFormat("\tint32 {0} = {1};\r\n", key, index);
            }
            else if (type == DataType.String)
            {
                str.AppendFormat("\tstring {0} = {1};\r\n", key, index);
            }
            else if (type == DataType.Array)
            {
                str.AppendFormat("\trepeated int32 {0} = {1};\r\n", key, index);
            }
        }

        public void ExportProto(ExcelSheet sheet)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < sheet.DataTypes.Count; i++)
            {
                DataType type = sheet.DataTypes[i];
                string key = sheet.DataKeys[i];
                int index = i + 1;
                AppendPbDef(str, type, key, index);
            }
            string file = string.Format(PBStr, sheet.SheetName, str.ToString());
            string protoPath = Path.Combine(Define.ProtoDir,string.Format("{0}.proto", sheet.SheetName));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }


        const string PbExportStr = @"using NPOI.SS.UserModel;

public class {0}Export
{{
    public void Export(ExcelReader reader)
    {{
		ISheet sheetData = reader.GetSheet(""{0}"");
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {{
            return;
        }}

        string sheetName = sheet.SheetName;
        TableProto.TB_{0} v = new TableProto.TB_{0}();
        for (int i = 0; i<sheet.DataRowCount; i++)
        {{
		    TableProto.{0} cfg = new TableProto.{0}();
            for (int j = 0; j<sheet.DataColCnt; j++)
            {{
                var field = sheet.DataValues[i][j];
{1}
            }}
            v.Data.Add(cfg);
            ProtoDataHandler.SaveProtoData(v, Define.ProtoBytesDir+'/'+sheetName+"".bin"");
        }}
    }}
}}
";

        private void AppendCSharpLine(StringBuilder str, DataType type,string key)
        {
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

                str.AppendFormat(@"\t\t\t\tvar t = ProtoDataExpoter.GetArrayFieldValue(field); 
                for(int m = 0;m<t.Length;m++)
                {{
                    cfg.{0}.Add(t[m]);
                }}",key);
            }
        }

        public void ExportCSharp(ExcelSheet sheet)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < sheet.DataTypes.Count; i++)
            {
                DataType type = sheet.DataTypes[i];
                string key = sheet.DataKeys[i];
                AppendCSharpLine(str, type, key);
            }

            string file = string.Format(PbExportStr, sheet.SheetName, str.ToString());
            string protoPath = Path.Combine(Define.ExporterDir, string.Format("{0}Export.cs", sheet.SheetName));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }

        const string RegisterStr = @"
public class ExcelExportRegister
{{
    public void ExportAllProtoData()
    {{
        ExcelReader reader = new ExcelReader();
        int readRet = reader.ReadAllTableExcel();
        if(readRet == 0)
        {{
{0}
        }}
    }}
}}
";

        private void AppendReigster(StringBuilder str, string sheetName,int index)
        {
            str.AppendFormat("\t\t\t{0}Export v{1} = new {0}Export();\r\n", sheetName, index);
            str.AppendFormat("\t\t\tv{0}.Export(reader);\r\n", index);
        }

        public void ExportRegister(ExcelReader reader)
        {
            var allSheets = reader.GetAllSheets();
            int cnt = 0;
            StringBuilder str = new StringBuilder();
            foreach(KeyValuePair<string,ISheet> kv in allSheets)
            {
                var sheet = kv.Value;
                AppendReigster(str, sheet.SheetName, cnt);
                cnt++;
            }

            string file = string.Format(RegisterStr, str.ToString());
            string protoPath = Path.Combine(Define.ExporterDir, string.Format("ExcelExportRegister.cs"));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }

        public void ExportTableReader(ExcelReader reader)
        {

        }
    }
}


