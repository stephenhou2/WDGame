using GameEngine;

public class BitTypeCreator
{
    public static BitType CreateModuleBitType(int index)
    {
        int maxSize = ModuleBitTypeQuery.Ins.GetBufferMaxSize();
        if (index < 0 || index >= maxSize)
        {
            Log.Error(ErrorLevel.Fatal, "CreateModuleBitType Failed, index:{0} out of range:[{1},{2}]", index, 0, maxSize);
            return null;
        }

        return new BitType(index, ModuleBitTypeQuery.Ins);
    }

    public static BitType CreateEventModuleBitType(int index)
    {
        int maxSize = EventBitTypeQuery.Ins.GetBufferMaxSize();
        if (index < 0 || index >= maxSize)
        {
            Log.Error(ErrorLevel.Fatal, "CreateModuleBitType Failed, index:{0} out of range:[{1},{2}]", index, 0, maxSize);
            return null;
        }

        return new BitType(index, EventBitTypeQuery.Ins);
    }
}



