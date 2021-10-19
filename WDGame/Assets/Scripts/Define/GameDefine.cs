using GameEngine;

public static class GameDefine
{
    public static string _UI_ROOT = "_UI_ROOT";
    public static string _MAP_EDITOR = "_MAP_EDITOR";
}

public static class CoreDefine
{
    public const int buffeSizeOfInt = sizeof(int) * 8;

    public const int BitTypeModuleBufferSize = 32; //最多支持64个模块定义
    public const int BitTypeEventBufferSize     = 128; //最多支持128种事件类型定义
    public const int BitTypeAgentStateBufferSize = 32;// 最多支持32中角色状态
}

public static class ModuleDef
{
    public static BitType SceneModule = BitTypeCreator.CreateModuleBitType(0);
    public static BitType InputModule = BitTypeCreator.CreateModuleBitType(1);
    public static BitType MapEditorModule = BitTypeCreator.CreateModuleBitType(2);
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

public static class AgentTypeDef
{
    public const int AGENT_TYPE_HERO = 0;
    public const int AGENT_TYPE_MONSTER = 1;
    public const int AGENT_TYPE_NPC = 1;
}

public class EquipDef
{
    public const int MaxEquipNum = 6;
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
