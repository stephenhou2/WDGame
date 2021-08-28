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

    /// <summary>
    /// panel对象缓存池
    /// </summary>
    private Dictionary<System.Type, UIPanel> mPanelPool;

    private UIManager()
    {
        mAllPanels = new List<UIPanel>();
        mUIRoot = GameObject.Find(GameDefine._UI_ROOT).gameObject;
        mPanelPool = new Dictionary<System.Type, UIPanel>();
    }

    private T GetPanel<T>() where T : UIPanel,new()
    {
        UIPanel panel = null;
        if(!mPanelPool.TryGetValue(typeof(T),out panel))
        {
            panel = new T();
        }
        return panel as T;
    }

    private void PushPanel<T>(UIPanel panel)
    {
        if(!mPanelPool.ContainsKey(typeof(T)))
        {
            mPanelPool.Add(typeof(T), panel);
        }
    }

    public void OpenPanel<T>(object[] openArgs = null) where T:UIPanel,new()
    {
        T panel = GetPanel<T>();
        if(!panel.CheckOpenArgs(openArgs))
            return;

        string panelPath = UIPathDef.GetUIPath<T>();
        if(string.IsNullOrEmpty(panelPath))
        {
            Log.Error("OpenPanel Failed, panel {0} does not registered!",typeof(T));
            return;
        }
        GameObject panelGo = LoadPanel(panelPath);
        panel.BindPanelNodes(panelGo);

        mAllPanels.Add(panel);
        panel.OnOpen(openArgs);
    }

    private void OpenPanel<T>(GameObject parentNode,object[] openArgs = null)
    {

    }

    public void AddControl<T>(GameObject parentNode, object[] openArgs = null) where T : UIPanel, new()
    {
        if (parentNode == null)
        {
            Log.Error("AddControl Error,parentNode is null !!!");
            return;
        }


    }

    public void ClosePanel<T>(T panel)
    {
        if (mAllPanels.Count == 0)
            return;

        int targetIndex = -1;
        for(int i = 0;i<mAllPanels.Count;i++)
        {
            if(mAllPanels[i].GetType().Equals(panel.GetType()))
            {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1)
            return;

        PushPanel<T>(mAllPanels[targetIndex]);
        RemovePanel(targetIndex);
    }

    private GameObject LoadPanel(string panelPath,GameObject parentNode)
    {
        if (string.IsNullOrEmpty(panelPath))
        {
            return null;
        }

        GameObject panelGo = null;
        GameObject obj = ResourceMgr.Load<GameObject>(panelPath, "Load UIPanel");
        if (obj != null)
        {
            panelGo = GameObject.Instantiate(obj);
            if(mUIRoot != null)
            {
                panelGo.transform.SetParent(mUIRoot.transform,false);
            }
        }
        else
        {
            Log.Error("LoadPanel Failed,panel go load failed,panelPath:{0}", panelPath);
        }

        return panelGo;
    }

    public void RemovePanel(int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= mAllPanels.Count)
            return;

        var panel = mAllPanels[targetIndex];
        if(panel != null)
        {
            panel.OnClose();
            panel.Clear();
            panel.DestroyPanelObj();
        }    

        mAllPanels.RemoveAt(targetIndex);
    }

    public void Update(float deltaTime)
    {
        for(int i = 0;i<mAllPanels.Count;i++)
        {
            UIPanel panel = mAllPanels[i];
            if(panel != null)
            {
                panel.Update(deltaTime);
            }
        }
    }
}
