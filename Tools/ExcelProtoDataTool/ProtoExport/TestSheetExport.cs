using NPOI.SS.UserModel;
public class TestSheetExport
{
    public void Export(ExcelReader reader)
    {
		ISheet sheetData = reader.GetSheet("TestSheet");
        if (sheetData == null)
            return;

        ExcelSheet sheet = new ExcelSheet();
        if(sheet.ReadExcelData(sheetData) < 0)
        {
            return;
        }
        string sheetName = sheet.SheetName;
		TableProto.TB_TestSheet v = new TableProto.TB_TestSheet();
         for (int i = 0; i < sheet.DataRowCount; i++)
         {
			TableProto.TestSheet cfg = new TableProto.TestSheet();
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
         ProtoDataHandler.SaveProtoData(v,Define.ProtoDir+'/'+sheetName+".bin");
        }
    }
}