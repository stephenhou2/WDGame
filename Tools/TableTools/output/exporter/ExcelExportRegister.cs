
public class ExcelExportRegister
{
    public void ExportAllProtoData()
    {
        ExcelReader reader = new ExcelReader();
        int readRet = reader.ReadAllTableExcel();
        if(readRet == 0)
        {
			TestSheetExport v0 = new TestSheetExport();
			v0.Export(reader);
			Test2Export v1 = new Test2Export();
			v1.Export(reader);
			MonsterCfgExport v2 = new MonsterCfgExport();
			v2.Export(reader);
			MonsterCfg222Export v3 = new MonsterCfg222Export();
			v3.Export(reader);

        }
    }
}
