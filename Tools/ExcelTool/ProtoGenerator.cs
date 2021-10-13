using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                    unitKey.AppendFormat(""|{{0}}"",  lineData[col]);
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
{1}
            tb.Data.Add(cfg);
        }}
        ProtoDataHandler.SaveProtoData(tb, Define.ProtoBytesDir+'/'+sheetName+"".bin"");
        return 0;
    }}
}}
";

        private void AppendExporterLine(StringBuilder str, DataType type, string key, int col)
        {
            if (type == DataType.Int)
            {
                str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetIntFieldValue(lineData[{1}]);\r\n", key, col);
            }
            else if (type == DataType.String)
            {
                str.AppendFormat("\t\t\t\tcfg.{0} = ProtoDataExpoter.GetStringFieldValue(lineData[{1}]);\r\n", key, col);
            }
            else if (type == DataType.Array)
            {

                str.AppendFormat(@"                 var t = ProtoDataExpoter.GetArrayFieldValue(lineData[{1}]); 
                for(int m = 0;m<t.Length;m++)
                {{
                    cfg.{0}.Add(t[m]);
                }}", key, col);
            }
        }

        public void ExportCSharp(string sheetName, List<FieldInfo> fieldInfos)
        {
            StringBuilder str = new StringBuilder();
            foreach (FieldInfo fi in fieldInfos)
            {
                if (fi.FieldType == FieldType.Comment || fi.FieldType == FieldType.Undefine)
                    continue;
                AppendExporterLine(str, fi.DataType, fi.FiledName, fi.Col);
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

        private void AppendReigster(StringBuilder str, string sheetName, int index)
        {
            str.AppendFormat(exportStr, sheetName, index);
        }

        public void ExportRegister(ExcelReader reader)
        {
            var allSheets = reader.GetAllSheets();
            int cnt = 0;
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, ISheet> kv in allSheets)
            {
                var sheet = kv.Value;
                AppendReigster(str, sheet.SheetName, cnt);
                cnt++;
            }

            string file = string.Format(RegisterStr, str.ToString());
            string protoPath = Path.Combine(Define.ExporterDir, string.Format("ExcelExportRegister.cs"));
            File.WriteAllText(protoPath, file, Encoding.UTF8);
        }

        const string DataTableReader = @"using System.Collections.Generic;

namespace TableProto
{{
    public partial class DataTables
    {{
        public static DataTables Ins;

        public static void CreateDataTables()
        {{
            Ins = new DataTables();
            Ins.LoadAllDataTables();
            Ins.LoadAllTableRedefines();
        }}

        private void LoadAllDataTables()
        {{
{0}
        }}

        public void ClearAllDataTables()
        {{
{1}
        }}

        public void LoadAllTableRedefines()
        {{
{2}
        }}
    }}
}}
";

        private void AppendLoadStr(StringBuilder str, string sheetName)
        {
            str.AppendFormat("\t\t\tLoad{0}();\r\n", sheetName);
        }

        private void AppendClearStr(StringBuilder str, string sheetName)
        {
            str.AppendFormat("\t\t\tClear{0}();\r\n", sheetName);
        }

        private void AppendRedefineLoadStr(StringBuilder str, string sheetName)
        {
            str.AppendFormat("\t\t\tLoad{0}Redefine();\r\n", sheetName);
        }

        const string singleTableReader = @"using System.Collections.Generic;

namespace TableProto
{{
    public partial class DataTables
    {{
{1}
        private int Load{0}()
        {{
            TB_{0} table = ProtoDataHandler.LoadProtoData<TB_{0}>(PathDefine.TABLE_PB_DATA_PATH + ""/{0}.bin"");
            if (table == null)
            {{
                return -1;
            }}

            foreach ({0} cfg in table.Data)
            {{
                tb_{0}.Add(cfg.{2}, cfg);
            }}

            return 0;
        }}

        public {0} Get{0}(int id)
        {{
            {0} cfg;
            if (tb_{0}.TryGetValue(id, out cfg))
            {{
                return cfg;
            }}

            Log.Error(ErrorLevel.Normal, ""Get {0} Failed, key = {{0}}"", id);
            return null;
        }}

{3}
{4} 

        public void Clear{0}()
        {{
            tb_{0} = null;
            {0}KeyMap = null;
        }}
    }}
}}
";

        private void AppendTableDicDefs(StringBuilder str, string sheetName)
        {
            str.AppendFormat(@"
        public Dictionary<int, {0}> tb_{0} = new Dictionary<int, {0}>();
        private Dictionary<string, int> {0}KeyMap = new Dictionary<string, int>();", sheetName);
        }

        private string GetUniqueKey(ExcelSheet es)
        {
            List<FieldInfo> fieldInfos = es.FieldInfos;
            for (int col = 0; col < fieldInfos.Count; col++)
            {
                FieldInfo fi = fieldInfos[col];
                if (fi.FieldType == FieldType.Unique)
                {
                    return fi.FiledName;
                }
            }

            return string.Empty;
        }



        const string RedefineFunc = @"
        public {0} Get{0}{1}({2})
        {{
            string key = string.Format(""{3}"",{4});
            int uniqueKey;
            if({0}KeyMap.TryGetValue(key,out uniqueKey))
            {{
                {0} cfg;
                if(tb_{0}.TryGetValue(uniqueKey,out cfg))
                {{
                    return cfg;
                }}
            }}
            else
            {{
                Log.Error(ErrorLevel.Critical,""Get{0}{1} Failed, unique key not find!"");
            }}

            return null;
        }}";

        private List<FieldInfo> mKeyFields = new List<FieldInfo>();

        private string GetTypeStr(DataType type)
        {
            if (type == DataType.Int)
                return "int";

            if (type == DataType.String)
                return "string";

            return string.Empty;
        }

        private string GetRedefineFunc(ExcelSheet es)
        {
            List<FieldInfo> fieldInfos = es.FieldInfos;

            mKeyFields.Clear();
            for (int col = 0; col < fieldInfos.Count; col++)
            {
                FieldInfo fi = fieldInfos[col];
                if (fi.FieldType == FieldType.Key && fi.DataType != DataType.Array && fi.DataType != DataType.Undefine)
                {
                    mKeyFields.Add(fi);
                }
            }

            if (mKeyFields.Count == 0)
                return string.Empty;

            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            StringBuilder s3 = new StringBuilder();
            StringBuilder s4 = new StringBuilder();
            for (int i = 0; i < mKeyFields.Count; i++)
            {
                s1.AppendFormat("_{0}", mKeyFields[i].FiledName);

                string typeStr = GetTypeStr(mKeyFields[i].DataType);
                if (!string.IsNullOrEmpty(typeStr))
                {
                    s2.AppendFormat("{0} {1},", typeStr, mKeyFields[i].FiledName);
                    s3.AppendFormat("{{{0}}}_", i);
                    s4.AppendFormat("{0},", mKeyFields[i].FiledName);
                }
            }

            if (s2.Length > 0)
                s2.Remove(s2.Length - 1, 1);

            if (s4.Length > 0)
                s4.Remove(s4.Length - 1, 1);

            return string.Format(RedefineFunc, es.SheetName, s1.ToString(), s2.ToString(), s3.ToString(), s4.ToString());
        }

        const string RedefineLoadFunc = @"
        private void Load{0}Redefine()
        {{
            {0}KeyMap.Clear();
            foreach (KeyValuePair<int, {0}> kv in tb_{0})
            {{
                var cfg = kv.Value;
                int uniqueKey = cfg.{1};
                string unitKey = string.Format(""{2}"", {3});
                if (!{0}KeyMap.ContainsKey(unitKey))
                {{
                    {0}KeyMap.Add(unitKey, uniqueKey);
                }}
                else
                {{
                    Log.Error(ErrorLevel.Critical, ""Load{0}Redefine Error, repeated unit key where unique key is {{0}}"", uniqueKey);
                }}
            }}
        }}";

        private string GetRedefineLoadFunc(ExcelSheet es)
        {
            List<FieldInfo> fieldInfos = es.FieldInfos;

            mKeyFields.Clear();
            string uniqueKey = string.Empty;
            for (int col = 0; col < fieldInfos.Count; col++)
            {
                FieldInfo fi = fieldInfos[col];
                if (fi.FieldType == FieldType.Key && fi.DataType != DataType.Array && fi.DataType != DataType.Undefine)
                {
                    mKeyFields.Add(fi);
                }

                if (fi.FieldType == FieldType.Unique)
                {
                    uniqueKey = fi.FiledName;
                }
            }

            if (mKeyFields.Count == 0)
                return string.Format(@"
         private void Load{0}Redefine(){{}}", es.SheetName);

            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            for (int i = 0; i < mKeyFields.Count; i++)
            {
                string typeStr = GetTypeStr(mKeyFields[i].DataType);
                if (!string.IsNullOrEmpty(typeStr))
                {
                    s1.AppendFormat("{{{0}}}_", i);
                    s2.AppendFormat("cfg.{0},", mKeyFields[i].FiledName);
                }
            }

            if (s1.Length > 0)
                s1.Remove(s1.Length - 1, 1);

            if (s2.Length > 0)
                s2.Remove(s2.Length - 1, 1);

            return string.Format(RedefineLoadFunc, es.SheetName, uniqueKey, s1.ToString(), s2.ToString());
        }


        private void ExportSingleTableReader(ISheet sheet)
        {
            StringBuilder tableDicDefs = new StringBuilder();
            AppendTableDicDefs(tableDicDefs, sheet.SheetName);
            ExcelSheet es = new ExcelSheet();
            es.ReadTableFieldDefineRow(sheet);

            string uniqueKey = GetUniqueKey(es);

            string redefineFunc = GetRedefineFunc(es);
            string redefineLoadFunc = GetRedefineLoadFunc(es);

            string file = string.Format(singleTableReader, sheet.SheetName, tableDicDefs.ToString(), uniqueKey, redefineFunc, redefineLoadFunc);
            string filePath = Path.Combine(Define.DataTableCSDir, string.Format("DataTables_{0}.cs", sheet.SheetName));
            File.WriteAllText(filePath, file, Encoding.UTF8);
        }

        public void ExportTableReader(ExcelReader reader)
        {
            var allSheets = reader.GetAllSheets();

            StringBuilder loadStr = new StringBuilder();
            StringBuilder clearStr = new StringBuilder();
            StringBuilder redefineLoadStr = new StringBuilder();
            foreach (KeyValuePair<string, ISheet> kv in allSheets)
            {
                var sheet = kv.Value;

                AppendLoadStr(loadStr, sheet.SheetName);
                AppendClearStr(clearStr, sheet.SheetName);
                AppendRedefineLoadStr(redefineLoadStr, sheet.SheetName);

                ExportSingleTableReader(sheet);
            }

            string file = string.Format(DataTableReader, loadStr.ToString(), clearStr.ToString(), redefineLoadStr.ToString());
            string filePath = Path.Combine(Define.DataTableCSDir, "DataTables.cs");
            File.WriteAllText(filePath, file, Encoding.UTF8);
        }
    }
}


