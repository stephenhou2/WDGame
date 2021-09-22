using NPOI.SS.UserModel;
public class MonsterCfg222Export
{
    public void Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("MonsterCfg222");
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            return;
        }
        string sheetName = sheet.SheetName;
		TableProto.TB_MonsterCfg222 v = new TableProto.TB_MonsterCfg222();
         for (int i = 0; i < sheet.DataRowCount; i++)
         {
			TableProto.MonsterCfg222 cfg = new TableProto.MonsterCfg222();
             for (int j = 0; j < sheet.DataColCnt; j++)
            {
                var field = sheet.DataValues[i][j];
				cfg.ID = ProtoDataExpoter.GetIntFieldValue(field);
				cfg.NAME = ProtoDataExpoter.GetStringFieldValue(field);
				cfg.ATK = ProtoDataExpoter.GetUIntFieldValue(field);
				cfg.HP = ProtoDataExpoter.GetUIntFieldValue(field);

            }
         v.Data.Add(cfg);
         ProtoDataHandler.SaveProtoData(v,Define.ProtoBytesDir+'/'+sheetName+".bin");
        }
    }
}