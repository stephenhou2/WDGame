using NPOI.SS.UserModel;

public class MonsterCfgExport
{
    public void Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("MonsterCfg");
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            return;
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
            ProtoDataHandler.SaveProtoData(v, Define.ProtoBytesDir+'/'+sheetName+".bin");
        }
    }
}
