using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 条件类型，根据需要自行扩展
/// </summary>
public static class PlotTriggerType
{
    public static string CurChapter = "CurChapter";

}

/// <summary>
/// 触发条件判断
/// </summary>
public static class PlotTriggerCompare
{
    public static string Equal = "=";
    public static string NotEqual = "!=";
    public static string LowerThan = "<";
    public static string GreaterThan = ">";
    public static string NotLowerThan = ">=";
    public static string NotGreaterThan = "<=";
    public static string In = "in";
    public static string NotIn = "not in";
    public static string Cross = "cross";
    public static string NotCross = "not cross";
}


public static class PlotTriggerHelper
{
    private static bool TriggerNullCheck(params object[] paramsArray)
    {
        for(int i=0;i<paramsArray.Length;i++)
        {
            object param = paramsArray[i];
            if(param == null)
            {
                Debug.LogErrorFormat("TriggeNullCheck: args{0} is null!", i);
                return false;
            }
        }

        return true;
    }

    private static bool TriggerNumberCheck(params string[] paramsArray)
    {
        for (int i = 0; i < paramsArray.Length; i++)
        {
            string param = paramsArray[i];
            if (!float.TryParse(param, out float _))
            {
                Debug.LogErrorFormat("TriggerNumberCheck: args{0} can not transfer to number", i);
                return false;
            }
        }

        return true;
    }

    public static bool TriggerEqual(string value1, string value2)
    {
        if(!TriggerNullCheck(value1,value2))
            return false;

        return value1.Equals(value2);
    }

    public static bool TriggerNotEqual(string value1, string value2)
    {
        return !TriggerEqual(value1, value2);
    }

    public static bool TriggerLowerThan(string value1,string value2)
    {
        if (!TriggerNullCheck(value1, value2))
            return false;

        if(!TriggerNumberCheck(value1,value2))
        {
            return false;
        }

        float v1 = float.Parse(value1);
        float v2 = float.Parse(value2);
        return v1 < v2;
    }

    public static bool TriggerGreaterThan(string value1, string value2)
    {
        if (!TriggerNullCheck(value1, value2))
            return false;

        if (!TriggerNumberCheck(value1, value2))
        {
            return false;
        }

        float v1 = float.Parse(value1);
        float v2 = float.Parse(value2);
        return v1 > v2;
    }

    public static bool TriggerNotLowerThan(string value1, string value2)
    {
        return !TriggerGreaterThan(value1, value2);
    }

    public static bool TriggerNotGreaterThan(string value1, string value2)
    {
        return !TriggerLowerThan(value1, value2);
    }

    public static bool TriggerIn(string value1, string[] valueArray)
    {
        if(!TriggerNullCheck(value1,valueArray))
            return false;

        for(int i=0;i<valueArray.Length;i++)
        {
            string tmp = valueArray[i];
            if(value1.Equals(tmp))
            {
                return true;
            }
        }

        return false;
    }

    public static bool TriggerNotIn(string value1,string[] valueArray)
    {
        return !TriggerIn(value1, valueArray);
    }

    public static bool TriggerCross(string[] array1,string[] array2)
    {
        if (!TriggerNullCheck(array1, array2))
            return false;

        for(int i=0;i<array1.Length;i++)
        {
            string v = array1[i];
            if(TriggerNullCheck(v))
            {
                for(int j=0;j<array2.Length;j++)
                {
                    if(v.Equals(array2[j]))
                    {
                        return true;
                    }
                }
            }
        }

        for(int i=0;i<array2.Length;i++)
        {
            string v = array2[i];
            if (TriggerNullCheck(v))
            {
                for (int j = 0; j < array1.Length; j++)
                {
                    if (v.Equals(array1[j]))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool TriggerNotCross(string[] array1,string[] array2)
    {
        return !TriggerCross(array1, array2);
    }
}
