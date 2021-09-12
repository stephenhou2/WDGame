using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Assertions;

public delegate void BitTypeHandle(BitType type);

public class BitType
{
    /*
     * 00000001 -module1
     * 00000010 -module2
     * 00000100 -module3 
     * ...
     */
    private int[] mBuffer;
    private string[] mTypeNames;
    
    public static BitType CreateBitType(int index,string name)
    {
        if (index < 0 || index >= CoreDefine.BitTypeMaxSize)
        {
            Log.Error(ErrorLevel.Fatal, "Create BitType Failed, index:{0} out of range:[{1},{2}]", index, 0, CoreDefine.BitTypeMaxSize);
            return null;
        }

        if (string.IsNullOrEmpty(name))
        {
            Log.Error(ErrorLevel.Fatal, "Create BitType Failed, type name is null or empty!");
            return null;
        }

        return new BitType(index, name);
    }

    private BitType(int index, string name)
    {
        Assert.IsTrue(index >= 0 && index < CoreDefine.BitTypeMaxSize);
        Assert.IsTrue(!string.IsNullOrEmpty(name));

        mBuffer = new int[CoreDefine.BitTypeBufferSize];
        mTypeNames = new string[CoreDefine.BitTypeBufferSize];

        int index_1 = index / sizeof(int);
        int index_2 = index % sizeof(int);

        int value = (int)(1 << index_2);


        if (index_1 >= 0 && index_1 < CoreDefine.BitTypeBufferSize)
        {
            mBuffer[index_1]= value;
            mTypeNames[index_1] = string.Format("[{0}]",name);
        }
        else
        {
            Debug.LogErrorFormat("BiTypeBuffer Create failed,index out of range,event name:{0}", name);
        }
    }
    private BitType(int[] buffer, string[] name)
    {
        mBuffer = buffer;
        mTypeNames = name;
    }

    private int[] GetTypeBuffer()
    {
        return mBuffer;
    }

    private string[] GetTypeNames()
    {
        return mTypeNames;
    }

    public string GetFullTypeName()
    {
        StringBuilder str = new StringBuilder();
        for (int i = 0; i < mTypeNames.Length; i++)
        {
            if(!string.IsNullOrEmpty(mTypeNames[i]))
            {
                str.AppendFormat("{0} ", mTypeNames[i]);
            }
        }
        return str.ToString();
    }

    public static BitType BindEventTypeBuffer(List<BitType> eventTypes)
    {
        int[] buffer = new int[CoreDefine.BitTypeBufferSize];
        string[] typeNames = new string[CoreDefine.BitTypeBufferSize];
        for(int i=0;i< eventTypes.Count; i++)
        {
            BitType src = eventTypes[i];
            for (int j = 0;j<CoreDefine.BitTypeBufferSize;j++)
            {
                buffer[j] |= src.GetTypeBuffer()[i];
                if(!string.IsNullOrEmpty(src.GetTypeNames()[j]))
                {
                    typeNames[j] += src.GetTypeNames()[j];
                }
            }
        }
        return new BitType(buffer, typeNames);
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

    public BitType Clone()
    {
        int[] buffer = new int[CoreDefine.BitTypeBufferSize];
        string[] typeNames = new string[CoreDefine.BitTypeBufferSize];

        for (int i = 0; i < CoreDefine.BitTypeBufferSize; i++)
        {
            buffer[i] = mBuffer[i];
            typeNames[i] = mTypeNames[i];
        }
        return new BitType(buffer, typeNames);
    }


    /// <summary>
    /// 遍历时用的类型
    /// </summary>
    private BitType mTempBitType;
    private BitType GetTempBitType()
    {
        if (mTempBitType == null)
        {
            int[] tempBuffer = new int[CoreDefine.BitTypeBufferSize];
            string[] tempTypeNames = new string[CoreDefine.BitTypeBufferSize];
            mTempBitType = new BitType(tempBuffer, tempTypeNames);
        }

        return mTempBitType;
    }

    /// <summary>
    /// 处理包含的所有类型，使用内部动态缓存，外部不能存储，用后即丢
    /// </summary>
    /// <param name="handle"></param>
    public void ForEachSingleType(BitTypeHandle handle)
    {
        if (handle == null)
            return;

        BitType bt = GetTempBitType();
        for (int i = 0;i<mBuffer.Length;i++)
        {
            int data = mBuffer[i];
            while(data > 0)
            {
                int bit = data & (~(data - 1)); // 取最后一位非零位的int值

                bt.mBuffer[i] = bit;
                bt.mTypeNames[i] = mTypeNames[i];
                handle(bt);

                // 清除数据
                bt.mBuffer[i] = 0;
                bt.mTypeNames[i] = null;

                // 剔除最后一位非零位
                data = data ^ bit;
            }
        }
    }


    /// <summary>
    /// 处理包含的所有类型，类型不复用，外部可以缓存，但是不推荐使用
    /// </summary>
    /// <param name="handle"></param>
    public void ForEachSingleTypeClone(BitTypeHandle handle)
    {
        if (handle == null)
            return;

        for (int i = 0; i < mBuffer.Length; i++)
        {
            int data = mBuffer[i];
            while (data > 0)
            {
                int bit = data & (~(data - 1)); // 取最后一位非零位的int值

                int[] buffer = new int[CoreDefine.BitTypeBufferSize];
                string[] typeNames = new string[CoreDefine.BitTypeBufferSize];

                buffer[i] = bit;
                typeNames[i] = mTypeNames[i];
                BitType bt = new BitType(buffer, typeNames);
                handle(bt);

                // 剔除最后一位非零位
                data = data ^ bit;
            }
        }
    }
}
