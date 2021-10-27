using System.Collections.Generic;

namespace TableProto
{
    public partial class DataTables
    {

        public Dictionary<int, HeroDataCfg> tb_HeroDataCfg = new Dictionary<int, HeroDataCfg>();
        private Dictionary<string, int> HeroDataCfgKeyMap = new Dictionary<string, int>();
        private int LoadHeroDataCfg()
        {
            TB_HeroDataCfg table = ProtoDataHandler.LoadProtoData<TB_HeroDataCfg>(PathDefine.TABLE_PB_DATA_PATH + "/HeroDataCfg.bin");
            if (table == null)
            {
                return -1;
            }

            foreach (HeroDataCfg cfg in table.Data)
            {
                tb_HeroDataCfg.Add(cfg.ID, cfg);
            }

            return 0;
        }

        public HeroDataCfg GetHeroDataCfg(int id)
        {
            HeroDataCfg cfg;
            if (tb_HeroDataCfg.TryGetValue(id, out cfg))
            {
                return cfg;
            }

            Log.Error(ErrorLevel.Normal, "Get HeroDataCfg Failed, key = {0}", id);
            return null;
        }



         private void LoadHeroDataCfgRedefine(){} 

        public void ClearHeroDataCfg()
        {
            tb_HeroDataCfg = null;
            HeroDataCfgKeyMap = null;
        }
    }
}
