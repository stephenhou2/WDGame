using GameEngine;

public static class GameDefine
{
    public static string _UI_ROOT = "_UI_ROOT";
    public static string _MAP_EDITOR = "_MAP_EDITOR";
}

public static class CoreDefine
{
    public const int BitTypeMaxSize = 128; //最多支持的类型数量
    public const int BitTypeBufferSize = BitTypeMaxSize / sizeof(int);
}

public static class ModuleDef
{
    public static BitType SceneModule = BitType.CreateBitType(0, "SceneModule");
    public static BitType InputModule = BitType.CreateBitType(1, "InputModule");
    public static BitType MapEditorModule = BitType.CreateBitType(2, "MapEditorModule");
}

public static class SceneDef
{
    public const string LoginScene = "Login";
    public const string MapEditorScene = "MapEditor";

}

public static class InputDef
{
    public const string MapEditorInputCtl = "MapEditorInput";
}

public static class CameraDef
{
    public const string MapEditorCamCtl = "MapEditorCamCtl";
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
