using System.Collections.Generic;

public static class UIPathDef
{
    private static Dictionary<System.Type, string> mUIPathDict;

    public const string UI_LAYER_BOTTOM_STATIC = "_UI_ROOT/_LAYER_BOTTOM/_STATIC";
    public const string UI_LAYER_BOTTOM_DYNAMIC = "_UI_ROOT/_LAYER_BOTTOM/_DYNAMIC";
    public const string UI_LAYER_NORMAL_STATIC = "_UI_ROOT/_LAYER_NORMAL/_STATIC";
    public const string UI_LAYER_NORMAL_DYNAMIC = "_UI_ROOT/_LAYER_NORMAL/_DYNAMIC";
    public const string UI_LAYER_MSG_STATIC = "_UI_ROOT/_LAYER_MSG/_STATIC";
    public const string UI_LAYER_MSG_DYNAMIC = "_UI_ROOT/_LAYER_MSG/_DYNAMIC";
    public const string UI_LAYER_TOP_STATIC = "_UI_ROOT/_LAYER_TOP/_STATIC";
    public const string UI_LAYER_TOP_DYNAMIC = "_UI_ROOT/_LAYER_TOP/_DYNAMIC";

    public static List<string> UI_ALL_LAYERS = new List<string>()
    {
        UI_LAYER_BOTTOM_STATIC,
        UI_LAYER_BOTTOM_DYNAMIC,
        UI_LAYER_NORMAL_STATIC,
        UI_LAYER_NORMAL_DYNAMIC,
        UI_LAYER_MSG_STATIC,
        UI_LAYER_MSG_DYNAMIC,
        UI_LAYER_TOP_STATIC,
        UI_LAYER_TOP_DYNAMIC
    };

    public static string GetUIPath<T>()
    {
        if (mUIPathDict == null)
            return string.Empty;

        string path;
        if (!mUIPathDict.TryGetValue(typeof(T), out path))
        {
            Log.Error(ErrorLevel.Critical, "GetUIPath Failed,no path registered,type:{0}", typeof(T));
            return string.Empty;
        }

        return path;
    }

    static UIPathDef()
    {
        mUIPathDict = new Dictionary<System.Type, string>();

        RegisterUIPath<Panel_MapEditor>("UI/MapEditor/Panel_MapEditor");
        RegisterUIPath<Panel_CreateNewMap>("UI/MapEditor/Panel_CreateNewMap");
    }

    private static void RegisterUIPath<T>(string path) where T:UIObject
    {
        if (string.IsNullOrEmpty(path))
            return;

        if(mUIPathDict.ContainsKey(typeof(T)))
        {
            Log.Error(ErrorLevel.Critical, "RegisterUIPath Error,Redefine ui path of type:{0}", typeof(T));
            return;
        }

        mUIPathDict.Add(typeof(T), path);
    }

}
