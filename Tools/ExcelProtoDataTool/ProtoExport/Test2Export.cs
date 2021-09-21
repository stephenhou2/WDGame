using NPOI.SS.UserModel;
public class Test2Export
{
    public void Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("Test2");
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            return;
        }
        string sheetName = sheet.SheetName;
		TableProto.TB_Test2 v = new TableProto.TB_Test2();
         for (int i = 0; i < sheet.DataRowCount; i++)
         {
			TableProto.Test2 cfg = new TableProto.Test2();
             for (int j = 0; j < sheet.DataColCnt; j++)
            {
                var field = sheet.DataValues[i][j];
				cfg.ID = ProtoDataExpoter.GetUIntFieldValue(field);
				cfg.Name = ProtoDataExpoter.GetStringFieldValue(field);
				cfg.Age = ProtoDataExpoter.GetUIntFieldValue(field);
               var t = ProtoDataExpoter.GetArrayFieldValue(field); 
                for(int m = 0;m<t.Length;m++)
                {
					cfg.GRADES.Add(t[m]);

				}
            }
        v.Data.Add(cfg);
         ProtoDataHandler.SaveProtoData(v,Define.ProtoBytesDir+'/'+sheetName+".bin");
        }
    }
}