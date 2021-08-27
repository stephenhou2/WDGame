using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager mIns;
    public static UIManager Ins
    {
        get
        { 
            if (mIns == null)
            {
                mIns = new UIManager();
            }

            return mIns;
        }
    }

    private List<UIPanel> mAllPanels;
    private GameObject mUIRoot;


    private UIManager()
    {
        mAllPanels = new List<UIPanel>();
        mUIRoot = GameObject.Find(GameDefine._UI_ROOT).gameObject;
    }

    //public void 

    private T LoadPanel<T>(string panelPath) where T:UIPanel
    {
        if (string.IsNullOrEmpty(panelPath))
        {
            return null;
        }

        T panel = null;
        if(FileHelper.FileExist(panelPath))
        {
            GameObject panelGo = ResourceMgr.Load<GameObject>(panelPath, "Load UIPanel");
            if (panelGo != null)
            {
                panel = panelGo.GetComponent<T>();
            }
            else
            {
                Log.Error("LoadPanel Failed,panel go load failed,panelPath:{0}", panelPath);
            }
        }
        else
        {
            Log.Error("LoadPanel Failed,panel path dont exist,panelPath:{0}", panelPath);
        }

        return panel;
    }

    public void RemovePanel(UIPanel panel)
    {

    }

}
