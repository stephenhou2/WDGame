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

        private void AppendPbDef(StringBuilder str, DataType type, string key, int index)
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

        public void ExportProto(string sheetName, List<FieldInfo> fieldInfos)
        {
            StringBuilder str = new StringBuilder();

            int index = 0;
            foreach (FieldInfo fi in fieldInfos)
            {
                if (fi.FieldType == FieldType.Comment || fi.FieldType == FieldType.Undefine)
                    continue;

                index++;
                AppendPbDef(str, fi.DataType, fi.FiledName, index);
            }

            string file = string.Format(PBStr, sheetName, str.ToString());
            string protoPath = Path.Combine(Define.ProtoDir, string.Format("{0}.proto", sheetName));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }


        const string PbExportStr = @"using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Text;

public class {0}Export
{{
    public string SheetName = ""{0}"";

    public int Export(ExcelReader reader)
    {{
		ISheet sheetData = reader.GetSheet(""{0}"");
        if (sheetData == null)
        {{
            string log = ""Export {0} Error, sheetData null!"";
            ConsoleLog.Error(log); 
            return -1;
        }}

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {{
            string log = ""Export {0} Error, ReadExcelData Failed!"";
            ConsoleLog.Error(log); 
            return -2;
        }}

        string sheetName = sheet.SheetName;
        TableProto.TB_{0} tb = new TableProto.TB_{0}();
        var lines = sheet.ExcelLines;
        var fieldInfos = sheet.FieldInfos;

        Dictionary<string, int> keyMap = new Dictionary<string, int>();
        Dictionary<int, int> uniqueKeyMap = new Dictionary<int, int>();
        StringBuilder unitKey = new StringBuilder();

        for(int row = 0;row<lines.Count;row++)
        {{
            List<string> lineData =  lines[row].lineData;
            if(lineData == null)
            {{
                string log = string.Format("" Export {0} Error, lineData null at row:{{0}}"",row);
                ConsoleLog.Error(log);
                return -3;
            }}
            
            unitKey.Clear();
            TableProto.{0} cfg = new TableProto.{0}();
            for (int col = 0;col < fieldInfos.Count;col++)
            {{
                FieldInfo fi = fieldInfos[col];
    
                if(fi.FieldType == FieldType.Comment && fi.FieldType == FieldType.Undefine)
                {{
                    continue;
                }}
                
                if(fi.FieldType == FieldType.Key)
                {{
                    unitKey.AppendFormat(""|{{0}}"", fi.FiledName);
                }}

                string cellStr = string.Empty;
                if (fi.FieldType == FieldType.Unique)
                {{
                    int uniqueKey = ProtoDataExpoter.GetIntFieldValue(lineData[col]);
                    if (uniqueKeyMap.ContainsKey(uniqueKey))
                    {{
                        string log = string.Format(""Export {0} Error, Unique key repeated at row:{{0}} and row:{{1}}"", uniqueKeyMap[uniqueKey]+2,row+2);
                        ConsoleLog.Error(log);
                        return -3;
                    }}
                    else
                    {{
                        uniqueKeyMap.Add(uniqueKey, row);
                    }}
                }}

                if (col >= 0 && col < lineData.Count)
                {{
                    cellStr = lineData[col];
                }}
{1}
            }}

            string uk = unitKey.ToString();
            if(!string.IsNullOrEmpty(uk))
            {{
                if (keyMap.ContainsKey(uk))
                {{
                    string log = string.Format(""Export {0} Error, United key repeated at row:{{0}} and row:{{1}}"", keyMap[uk]+2, row+2);
                    ConsoleLog.Error(log);
                    return -3;
                }}
                else
                {{
                    keyMap.Add(uk, row);
                }}
            }}

            tb.Data.Add(cfg);
        }}
        ProtoDataHandler.SaveProtoData(tb, Define.ProtoBytesDir+'/'+sheetName+"".bin"");
        return 0;
    }}
}}
";

        private void AppendExporterLine(StringBuilder str, DataType type,string key)
        {
            if (type == DataType.UInt)
            {
                str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetUIntFieldValue(cellStr);\r\n", key);
            }
            else if (type == DataType.Int)
            {
                str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetIntFieldValue(cellStr);\r\n", key);
            }
            else if (type == DataType.String)
            {
                str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetStringFieldValue(cellStr);\r\n", key);
            }
            else if (type == DataType.Array)
            {

                str.AppendFormat(@"                 var t = ProtoDataExpoter.GetArrayFieldValue(cellStr); 
                for(int m = 0;m<t.Length;m++)
                {{
                    cfg.{0}.Add(t[m]);
                }}", key);
            }
        }

        public void ExportCSharp(string sheetName, List<FieldInfo> fieldInfos)
        {
            StringBuilder str = new StringBuilder();
            foreach (FieldInfo fi in fieldInfos)
            {
                if (fi.FieldType == FieldType.Comment || fi.FieldType == FieldType.Undefine)
                    continue;
                AppendExporterLine(str, fi.DataType, fi.FiledName);
            }

            string file = string.Format(PbExportStr, sheetName, str.ToString());
            string protoPath = Path.Combine(Define.ExporterDir, string.Format("{0}Export.cs", sheetName));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }

        const string RegisterStr = @"
public class ExcelExportRegister
{{
    public int ExportAllProtoData()
    {{
        string log = string.Empty;
        ExcelReader reader = new ExcelReader();
        int readRet = reader.ReadAllTableExcel();
        if(readRet < 0)
        {{
            log = string.Format(""ExportAllProtoData, ReadAllTableExcel failed"");
            ConsoleLog.Error(log); 
            return -1;
        }}
{0}
        return 0;
    }}
}}";

        const string exportStr = @"	    {0}Export v{1} = new {0}Export();
		if(v{1}.Export(reader) < 0)
        {{
            log = ""Export Proto Data failed, sheet : {0}"";
            ConsoleLog.Error(log);
            return -2;
        }}
";

        private void AppendReigster(StringBuilder str, string sheetName,int index)
        {
            str.AppendFormat(exportStr, sheetName, index);
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


