using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Log
{
    public static void Logic(string str)
    {
        Debug.Log(str);
    }    
    
    public static void Logic(string str, params object[] args)
    {
        Debug.LogFormat(str,args);
    }

    public static void Error(string str)
    {
        Debug.LogError(str);
    }

    public static void Error(string str, params object[] args)
    {
        Debug.LogErrorFormat(str,args);
    }

    public static void Warning(string str)
    {
        Debug.LogWarning(str);
    }

    public static void Warning(string str, params object[] args)
    {
        Debug.LogWarningFormat(str,args);
    }
}
