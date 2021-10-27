
public static class BasePropertyHelper
{
    public static BaseProperty CreateHeroBaseProperty(int heroId)
    {
        BaseProperty bp = new BaseProperty();

        var heroCfg = TableProto.DataTables.Ins.GetHeroDataCfg(heroId);

        //bp.HP = heroCfg.
        

        return bp;
    }
}
