using System.Collections.Generic;

namespace TableProto
{
    public partial class DataTables
    {

        public Dictionary<int, TestSheet> tb_TestSheet = new Dictionary<int, TestSheet>();
        private Dictionary<string, int> TestSheetKeyMap = new Dictionary<string, int>();
        private int LoadTestSheet()
        {
            TB_TestSheet table = ProtoDataHandler.LoadProtoData<TB_TestSheet>(PathDefine.TABLE_PB_DATA_PATH + "/TestSheet.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (TestSheet cfg in table.Data)
            {
                tb_TestSheet.Add(cfg.ID, cfg);
            }

            return 0;
        }

        public TestSheet GetTestSheet(int id)
        {
            TestSheet cfg;
            if (tb_TestSheet.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal, "Get TestSheet Failed, key = {0}", id);
            return null;
        }


        public TestSheet GetTestSheet_Name_Age(string Name,int Age)
        {
            string key = string.Format("{0}_{1}_",Name,Age);
            int uniqueKey;
            if(TestSheetKeyMap.TryGetValue(key,out uniqueKey))
            {
                TestSheet cfg;
                if(tb_TestSheet.TryGetValue(uniqueKey,out cfg))
                {
                    return cfg;
                }
            }
            else
            {
                Log.Error(ErrorLevel.Critical,"GetTestSheet_Name_Age Failed, unique key not find!");
            }

            return null;
        }

        private void LoadTestSheetRedefine()
        {
            TestSheetKeyMap.Clear();
            foreach (KeyValuePair<int, TestSheet> kv in tb_TestSheet)
            {
                var cfg = kv.Value;
                int uniqueKey = cfg.ID;
                string unitKey = string.Format("{0}_{1}", cfg.Name,cfg.Age);
                if (!TestSheetKeyMap.ContainsKey(unitKey))
                {
                    TestSheetKeyMap.Add(unitKey, uniqueKey);
                }
                else
                {
                    Log.Error(ErrorLevel.Critical, "LoadTestSheetRedefine Error, repeated unit key where unique key is {0}", uniqueKey);
                }
            }
        } 

        public void ClearTestSheet()
        {
            tb_TestSheet = null;
            TestSheetKeyMap = null;
        }
    }
}
