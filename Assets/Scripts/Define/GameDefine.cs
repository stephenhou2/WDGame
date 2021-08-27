using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    public static string _UI_ROOT = "_UI_ROOT";

}


public class PlatformDef
{
    public const string Android = "android";
    public const string Ios = "ios";
    public const string Windows = "windows";
    public const string Osx = "Osx";

#if UNITY_ANDROID && (!UNITY_EDITOR)
    public const string Current = Android;    
#elif UNITY_IOS && (!UNITY_EDITOR)
    public const string Current = Ios;
#else
    public const string Current = Windows;
#endif

}
