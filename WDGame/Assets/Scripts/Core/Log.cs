using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorLevel
{    
    Fatal = 0,  //致命错误
    Critical = 1, // 严重错误
    Normal = 2, // 一般错误
    Hint = 3, // 提示性错误
    Undefine = 4,
}

public enum LogLevel
{
    Critical = 0, 
    Normal = 1, 
    Hint = 2,
    Undefine = 3,
}

public static class Log
{
    public static void Logic(string str)
    {
        Debug.Log(str);
    }    
    
    public static void Logic(LogLevel level,string str, params object[] args)
    {
        Debug.LogFormat(str,args);
    }

    /// <summary>
    /// log error
    /// </summary>
    /// <param name="str"></param>
    /// <param name="level">0级:致命错误  1:严重错误  2:一般错误  3: 提示性错误</param>
    public static void Error(ErrorLevel level, string str)
    {
        Debug.LogError(str);
    }

    public static void Error(ErrorLevel level, string str, params object[] args)
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
