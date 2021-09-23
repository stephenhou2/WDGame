using System.Collections.Generic;

namespace TableProto
{
    public partial class DataTables
    {

        public Dictionary<int, Test2> tb_Test2 = new Dictionary<int, Test2>();
        private Dictionary<string, int> Test2KeyMap = new Dictionary<string, int>();
        private int LoadTest2()
        {
            TB_Test2 table = ProtoDataHandler.LoadProtoData<TB_Test2>(PathDefine.TABLE_PB_DATA_PATH + "/Test2.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (Test2 cfg in table.Data)
            {
                tb_Test2.Add(cfg.ID, cfg);
            }

            return 0;
        }

        public Test2 GetTest2(int id)
        {
            Test2 cfg;
            if (tb_Test2.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal, "Get Test2 Failed, key = {0}", id);
            return null;
        }


        public Test2 GetTest2_Name_Age(string Name,int Age)
        {
            string key = "Name_Age_";
            int uniqueKey;
            if(Test2KeyMap.TryGetValue(key,out uniqueKey))
            {
                Test2 cfg;
                if(tb_Test2.TryGetValue(uniqueKey,out cfg))
                {
                    return cfg;
                }
            }
            else
            {
                Log.Error(ErrorLevel.Critical,"GetTest2_Name_Age Failed, unique key not find!");
            }

            return null;
        }

        public void ClearTest2()
        {
            tb_Test2 = null;
            Test2KeyMap = null;
        }
    }
}
