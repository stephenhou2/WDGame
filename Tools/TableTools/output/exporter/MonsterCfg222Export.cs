using NPOI.SS.UserModel;

public class MonsterCfg222Export
{
    public string SheetName = "MonsterCfg222";

    public int Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("MonsterCfg222");
        if (sheetData == null)
        {
            string log = "Export MonsterCfg222 Error, sheetData null!";
            ConsoleLog.Error(log); 
            return -1;
        }

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            string log = "Export MonsterCfg222 Error, ReadExcelData Failed!";
            ConsoleLog.Error(log); 
            return -2;
        }

        string sheetName = sheet.SheetName;
        TableProto.TB_MonsterCfg222 v = new TableProto.TB_MonsterCfg222();
        for (int i = 0; i<sheet.DataRowCount; i++)
        {
		    TableProto.MonsterCfg222 cfg = new TableProto.MonsterCfg222();
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
