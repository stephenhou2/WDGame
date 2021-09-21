using System;

public static class ConsoleLog
{
    public static void Log(string str)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(str);
    }

    public static void Error(string str)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
