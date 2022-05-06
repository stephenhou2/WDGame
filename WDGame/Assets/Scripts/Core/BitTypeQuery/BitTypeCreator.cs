using GameEngine;

public class BitTypeCreator
{
    public static BitType CreateModuleBitType(int index)
    {
        int maxSize = ModuleBitTypeQuery.Ins.GetBufferMaxSize();
        if (index < 0 || index >= maxSize * CoreDefine.buffeSizeOfInt)
        {
            Log.Error(ErrorLevel.Fatal, "CreateModuleBitType Failed, index:{0} out of range:[{1},{2}]", index, 0, maxSize);
            return null;
        }

        return new BitType(index, ModuleBitTypeQuery.Ins, false);
    }

    public static BitType CreateEventModuleBitType(int index)
    {
        int maxSize = EventBitTypeQuery.Ins.GetBufferMaxSize();
        if (index < 0 || index >= maxSize * CoreDefine.buffeSizeOfInt)
        {
            Log.Error(ErrorLevel.Fatal, "CreateModuleBitType Failed, index:{0} out of range:[{1},{2}]", index, 0, maxSize);
            return null;
        }

        return new BitType(index, EventBitTypeQuery.Ins, false);
    }


    public static BitType CreateAgentStateBitType(int index)
    {
        int maxSize = EventBitTypeQuery.Ins.GetBufferMaxSize();
        if (index < 0 || index >= maxSize * CoreDefine.buffeSizeOfInt)
        {
            Log.Error(ErrorLevel.Fatal, "CreateAgentStateBitType Failed, index:{0} out of range:[{1},{2}]", index, 0, maxSize);
            return null;
        }

        return new BitType(index, AgentStateBitTypeQuery.Ins, true);

    }
}



