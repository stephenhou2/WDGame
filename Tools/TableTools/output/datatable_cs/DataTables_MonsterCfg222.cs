using System.Collections.Generic;

namespace TableProto
{
    public partial class DataTables
    {

        public Dictionary<int, MonsterCfg222> tb_MonsterCfg222 = new Dictionary<int, MonsterCfg222>();
        private Dictionary<string, int> MonsterCfg222KeyMap = new Dictionary<string, int>();
        private int LoadMonsterCfg222()
        {
            TB_MonsterCfg222 table = ProtoDataHandler.LoadProtoData<TB_MonsterCfg222>(PathDefine.TABLE_PB_DATA_PATH + "/MonsterCfg222.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (MonsterCfg222 cfg in table.Data)
            {
                tb_MonsterCfg222.Add(cfg.ID, cfg);
            }

            return 0;
        }

        public MonsterCfg222 GetMonsterCfg222(int id)
        {
            MonsterCfg222 cfg;
            if (tb_MonsterCfg222.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal, "Get MonsterCfg222 Failed, key = {0}", id);
            return null;
        }



        public void ClearMonsterCfg222()
        {
            tb_MonsterCfg222 = null;
            MonsterCfg222KeyMap = null;
        }
    }
}
