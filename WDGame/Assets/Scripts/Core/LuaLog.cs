using XLua;

[LuaCallCSharp]
public static class LuaLog
{
    public static void Logic(int logLevel,string str)
    {
        if (logLevel < (int)LogLevel.Undefine)
        {
            Log.Logic((LogLevel)logLevel, str);
        }
        else
        {
            Log.Error(ErrorLevel.Hint, "LuaLog undefined logLevel!");
            Log.Logic(LogLevel.Undefine, str);
        }
    }

    public static void Error(int errorLevel,string str)
    {
        if (errorLevel < (int)ErrorLevel.Undefine)
        {
            Log.Error((ErrorLevel)errorLevel, str);
        }
        else
        {
            Log.Error(ErrorLevel.Hint, "LuaLog undefined errorLevel!");
            Log.Error(ErrorLevel.Undefine, str);
        }
    }
}
