using System.Collections.Generic;

namespace TableProto
{
    public partial class DataTables
    {

        public Dictionary<int, MonsterCfg> tb_MonsterCfg = new Dictionary<int, MonsterCfg>();
        private Dictionary<string, int> MonsterCfgKeyMap = new Dictionary<string, int>();
        private int LoadMonsterCfg()
        {
            TB_MonsterCfg table = ProtoDataHandler.LoadProtoData<TB_MonsterCfg>(PathDefine.TABLE_PB_DATA_PATH + "/MonsterCfg.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (MonsterCfg cfg in table.Data)
            {
                tb_MonsterCfg.Add(cfg.ID, cfg);
            }

            return 0;
        }

        public MonsterCfg GetMonsterCfg(int id)
        {
            MonsterCfg cfg;
            if (tb_MonsterCfg.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal, "Get MonsterCfg Failed, key = {0}", id);
            return null;
        }



         private void LoadMonsterCfgRedefine(){} 

        public void ClearMonsterCfg()
        {
            tb_MonsterCfg = null;
            MonsterCfgKeyMap = null;
        }
    }
}
