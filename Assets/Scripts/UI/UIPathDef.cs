using System.Collections.Generic;

public static class UIPathDef
{
    public const string UI_LAYER_BOTTOM_STATIC = "_UI_ROOT/_LAYER_BOTTOM/_STATIC";
    public const string UI_LAYER_BOTTOM_DYNAMIC = "_UI_ROOT/_LAYER_BOTTOM/_DYNAMIC";
    public const string UI_LAYER_NORMAL_STATIC = "_UI_ROOT/_LAYER_NORMAL/_STATIC";
    public const string UI_LAYER_NORMAL_DYNAMIC = "_UI_ROOT/_LAYER_NORMAL/_DYNAMIC";
    public const string UI_LAYER_MSG_STATIC = "_UI_ROOT/_LAYER_MSG/_STATIC";
    public const string UI_LAYER_MSG_DYNAMIC = "_UI_ROOT/_LAYER_MSG/_DYNAMIC";
    public const string UI_LAYER_TOP_STATIC = "_UI_ROOT/_LAYER_TOP/_STATIC";
    public const string UI_LAYER_TOP_DYNAMIC = "_UI_ROOT/_LAYER_TOP/_DYNAMIC";

    public static List<string> ALL_UI_LAYER = new List<string>()
    {
        UI_LAYER_BOTTOM_STATIC,
        UI_LAYER_BOTTOM_DYNAMIC,
        UI_LAYER_NORMAL_STATIC,
        UI_LAYER_NORMAL_DYNAMIC,
        UI_LAYER_MSG_STATIC,
        UI_LAYER_MSG_DYNAMIC,
        UI_LAYER_TOP_STATIC,
        UI_LAYER_TOP_DYNAMIC,
    };


    private static Dictionary<System.Type, string> mPanelPathDict = new Dictionary<System.Type, string>();

    public static void RegisterUIPanel(System.Type panelType,string panelPath)
    {
        if (mPanelPathDict.ContainsKey(panelType))
        {
            Log.Error(ErrorLevel.Fatal, "RegisterUIPanel Failed,{0} already registered!");
            return;
        }

        Log.Error(ErrorLevel.Hint,"RegisterUIPanel {0}-{1}", panelType, panelPath);
        mPanelPathDict.Add(panelType, panelPath);
    }

}