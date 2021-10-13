using GameEngine;

public class ModuleBitTypeQuery : IBitTypeQuery
{
    private ModuleBitTypeQuery() { }

    private static ModuleBitTypeQuery _ins;
    public static ModuleBitTypeQuery Ins
    {
        get
        {
            if(_ins == null)
            {
                _ins = new ModuleBitTypeQuery();
            }

            return _ins;
        }
    }


    public string BitTypeTranslate(BitType bt)
    {
        return string.Empty;
    }

    public int GetBufferMaxSize()
    {
        return CoreDefine.BitTypeModuleBufferSize / sizeof(int);
    }
}
