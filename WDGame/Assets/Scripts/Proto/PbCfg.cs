using System;
using System.Collections.Generic;
using System.Text;

namespace TableProto
{
    public class PbCfg
    {
        public static PbCfg Ins;

        public Dictionary<int, MonsterCfg> mMonsterCfgs = new Dictionary<int, MonsterCfg>();
        //private Dictionary<string,int> monsterCfgKeyMap = 

        public static void CreatePbCfg()
        {
            Ins = new PbCfg();
            Ins.LoadAllPbCfgs();
        }

        private int LoadMonsterCfg()
        {
            TB_MonsterCfg table = ProtoDataHandler.LoadProtoData<TB_MonsterCfg>(PathDefine.TABLE_PB_DATA_PATH + "/MonsterCfg.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (MonsterCfg cfg in (table.Data))
            {

            }

            return 0;
        }

        private void LoadAllPbCfgs()
        {
            
        }

        public static void ClearPbCfg()
        {

        }

        public MonsterCfg GetMonsterCfg(int id)
        {
            MonsterCfg cfg;
            if (mMonsterCfgs.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal,"Get MonsterCfg Failed, key = {0}", id);
            return null;
        }
    }
}
