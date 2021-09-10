using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Assertions;

public class BitType
{
    /*
     * 00000001 -module1
     * 00000010 -module2
     * 00000100 -module3 
     * ...
     */
    private int[] mBuffer;
    private string mTypeName;
    
    public BitType(int index, string name)
    {
        Assert.IsTrue(index >= 0 && index < CoreDefine.BitTypeMaxSize);

        if (index < 0 || index >=CoreDefine.BitTypeMaxSize)
        {
            return;
        }

        mBuffer = new int[CoreDefine.BitTypeBufferSize];

        int index_1 = index / sizeof(int);
        int index_2 = index % sizeof(int);

        int value = (int)(1 << index_2);

        mTypeName = name;

        if (index_1 >= 0 && index_1 < CoreDefine.BitTypeBufferSize)
        {
            mBuffer[index_1]= value;
        }
        else
        {
            Debug.LogErrorFormat("BiTypeBuffer Create failed,index out of range,event name:{0}", name);
        }
    }
    private BitType(int[] buffer, string name)
    {
        mBuffer = buffer;
        mTypeName = name;
    }

    private int[] GetTypeBuffer()
    {
        return mBuffer;
    }

    public string GetEventName()
    {
        return mTypeName;
    }

    public static BitType BindEventTypeBuffer(List<BitType> eventTypes)
    {
        int[] buffer = new int[CoreDefine.BitTypeBufferSize];
        StringBuilder s = new StringBuilder();
        for(int i=0;i< eventTypes.Count; i++)
        {
            BitType src = eventTypes[i];
            s.AppendFormat("[{0}]",src.GetEventName());
            for (int j = 0;j<CoreDefine.BitTypeBufferSize;j++)
            {
                buffer[j] |= src.GetTypeBuffer()[i];
            }
        }
        return new BitType(buffer, s.ToString());
    }

    public bool HasEvent(BitType evt)
    {
        for (int i = 0; i < CoreDefine.BitTypeBufferSize; i++)
        {
            int temp = mBuffer[i] &= evt.GetTypeBuffer()[i];
            if (temp > 0)
                return true;
        }

        return false;
    }
}
