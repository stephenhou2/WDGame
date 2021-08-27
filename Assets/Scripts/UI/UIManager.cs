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

    public void OpenPanel<T>(string panelPath,object[] openArgs = null) where T:UIPanel,new()
    {
        T panel = LoadPanel<T>(panelPath);
        if (panel == null)
            return;

        mAllPanels.Add(panel);
        panel.OnOpen(openArgs);
    }

    public void ClosePanel<T>()
    {
        if (mAllPanels.Count == 0)
            return;

        int targetIndex = 0;
        for(int i = 0;i<mAllPanels.Count;i++)
        {
            if(mAllPanels[i].GetType().Equals(typeof(T)))
            {
                targetIndex = i;
                break;
            }
        }

        RemovePanel(targetIndex);
    }

    private T LoadPanel<T>(string panelPath) where T:UIPanel,new()
    {
        if (string.IsNullOrEmpty(panelPath))
        {
            return null;
        }

        T panel = null;
        GameObject obj = ResourceMgr.Load<GameObject>(panelPath, "Load UIPanel");
        if (obj != null)
        {
            GameObject panelGo = GameObject.Instantiate(obj);
            if(mUIRoot != null)
            {
                panelGo.transform.SetParent(mUIRoot.transform,false);
            }
            panel = new T();
            panel.BindPanelRootNode(panelGo);
        }
        else
        {
            Log.Error("LoadPanel Failed,panel go load failed,panelPath:{0}", panelPath);
        }

        return panel;
    }

    public void RemovePanel(int targetIndex)
    {
        if (targetIndex < mAllPanels.Count)
            return;

        mAllPanels[targetIndex].OnClose();
        mAllPanels.RemoveAt(targetIndex);
    }

}
