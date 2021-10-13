using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Text;

public class MonsterCfgExport
{
    public string SheetName = "MonsterCfg";

    public int Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("MonsterCfg");
        if (sheetData == null)
        {
            string log = "Export MonsterCfg Error, sheetData null!";
            ConsoleLog.Error(log); 
            return -1;
        }

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            string log = "Export MonsterCfg Error, ReadExcelData Failed!";
            ConsoleLog.Error(log); 
            return -2;
        }

        string sheetName = sheet.SheetName;
        TableProto.TB_MonsterCfg tb = new TableProto.TB_MonsterCfg();
        var lines = sheet.ExcelLines;
        var fieldInfos = sheet.FieldInfos;

        Dictionary<string, int> keyMap = new Dictionary<string, int>();
        Dictionary<int, int> uniqueKeyMap = new Dictionary<int, int>();
        StringBuilder unitKey = new StringBuilder();

        for(int row = 0;row<lines.Count;row++)
        {
            List<string> lineData =  lines[row].lineData;
            if(lineData == null)
            {
                string log = string.Format(" Export MonsterCfg Error, lineData null at row:{0}",row);
                ConsoleLog.Error(log);
                return -3;
            }
            
            unitKey.Clear();
            TableProto.MonsterCfg cfg = new TableProto.MonsterCfg();
            for (int col = 0;col < fieldInfos.Count;col++)
            {
                FieldInfo fi = fieldInfos[col];
    
                if(fi.FieldType == FieldType.Comment && fi.FieldType == FieldType.Undefine)
                {
                    continue;
                }
                
                if(fi.FieldType == FieldType.Key)
                {
                    unitKey.AppendFormat("|{0}",  lineData[col]);
                }

                string cellStr = string.Empty;
                if (fi.FieldType == FieldType.Unique)
                {
                    int uniqueKey = ProtoDataExpoter.GetIntFieldValue(lineData[col]);
                    if (uniqueKeyMap.ContainsKey(uniqueKey))
                    {
                        string log = string.Format("Export MonsterCfg Error, Unique key repeated at row:{0} and row:{1}", uniqueKeyMap[uniqueKey]+2,row+2);
                        ConsoleLog.Error(log);
                        return -3;
                    }
                    else
                    {
                        uniqueKeyMap.Add(uniqueKey, row);
                    }
                }
            }

            string uk = unitKey.ToString();
            if(!string.IsNullOrEmpty(uk))
            {
                if (keyMap.ContainsKey(uk))
                {
                    string log = string.Format("Export MonsterCfg Error, United key repeated at row:{0} and row:{1}", keyMap[uk]+2, row+2);
                    ConsoleLog.Error(log);
                    return -3;
                }
                else
                {
                    keyMap.Add(uk, row);
                }
            }
				cfg.ID = ProtoDataExpoter.GetIntFieldValue(lineData[0]);
				cfg.NAME = ProtoDataExpoter.GetStringFieldValue(lineData[1]);
				cfg.ATK = ProtoDataExpoter.GetIntFieldValue(lineData[2]);
				cfg.HP = ProtoDataExpoter.GetIntFieldValue(lineData[3]);

            tb.Data.Add(cfg);
        }
        ProtoDataHandler.SaveProtoData(tb, Define.ProtoBytesDir+'/'+sheetName+".bin");
        return 0;
    }
}
