using NPOI.SS.UserModel;

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
        TableProto.TB_MonsterCfg v = new TableProto.TB_MonsterCfg();
        for (int i = 0; i<sheet.DataRowCount; i++)
        {
		    TableProto.MonsterCfg cfg = new TableProto.MonsterCfg();
            for (int j = 0; j<sheet.DataColCnt; j++)
            {
                var field = sheet.DataValues[i][j];
				cfg.ID = ProtoDataExpoter.GetIntFieldValue(field);
				cfg.NAME = ProtoDataExpoter.GetStringFieldValue(field);
				cfg.ATK = ProtoDataExpoter.GetUIntFieldValue(field);
				cfg.HP = ProtoDataExpoter.GetUIntFieldValue(field);

            }
            v.Data.Add(cfg);
        }
        ProtoDataHandler.SaveProtoData(v, Define.ProtoBytesDir+'/'+sheetName+".bin");
        return 0;
    }
}
