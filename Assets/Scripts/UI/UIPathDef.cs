using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIPathDef
{
    public static string GetUIPath<T>()
    {
        if (mUIPathDict == null)
            return string.Empty;

        string path;
        if (!mUIPathDict.TryGetValue(typeof(T), out path))
        {
            Log.Error("GetUIPath Failed,no path registered,type:{0}", typeof(T));
            return  string.Empty;
        }

        return path;
    }

    private static Dictionary<System.Type, string> mUIPathDict;

    static UIPathDef()
    {
        mUIPathDict = new Dictionary<System.Type, string>();

        RegisterUIPath<Panel_MapEditor>("UI/MapEditor/Panel_MapEditor");
        RegisterUIPath<Panel_CreateNewMap>("UI/MapEditor/Panel_CreateNewMap");
    }

    private static void RegisterUIPath<T>(string path) where T:UIPanel
    {
        if (string.IsNullOrEmpty(path))
            return;

        if(mUIPathDict.ContainsKey(typeof(T)))
        {
            Log.Error("RegisterUIPath Error,Redefine ui path of type:{0}", typeof(T));
            return;
        }

        mUIPathDict.Add(typeof(T), path);
    }

}
