
public class ExcelExportRegister
{
    public int ExportAllProtoData()
    {
        string log = string.Empty;
        ExcelReader reader = new ExcelReader();
        int readRet = reader.ReadAllTableExcel();
        if(readRet < 0)
        {
            log = string.Format("ExportAllProtoData, ReadAllTableExcel failed");
            ConsoleLog.Error(log); 
            return -1;
        }
	    TestSheetExport v0 = new TestSheetExport();
		if(v0.Export(reader) < 0)
        {
            log = "Export Proto Data failed, sheet : TestSheet";
            ConsoleLog.Error(log);
            return -2;
        }
	    Test2Export v1 = new Test2Export();
		if(v1.Export(reader) < 0)
        {
            log = "Export Proto Data failed, sheet : Test2";
            ConsoleLog.Error(log);
            return -2;
        }
	    MonsterCfgExport v2 = new MonsterCfgExport();
		if(v2.Export(reader) < 0)
        {
            log = "Export Proto Data failed, sheet : MonsterCfg";
            ConsoleLog.Error(log);
            return -2;
        }
	    MonsterCfg222Export v3 = new MonsterCfg222Export();
		if(v3.Export(reader) < 0)
        {
            log = "Export Proto Data failed, sheet : MonsterCfg222";
            ConsoleLog.Error(log);
            return -2;
        }

        return 0;
    }
}