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

    Dictionary<string, GameObject> mLayers;

    /// <summary>
    /// panel对象缓存池
    /// </summary>
    private Dictionary<System.Type, UIObject> mPanelPool;

    private void RegisterLayer(string layerPath)
    {
        if (string.IsNullOrEmpty(layerPath))
            return;

        var layer = GameObject.Find(layerPath);
        if (layer != null)
            mLayers.Add(layerPath, layer);
    }

    private GameObject GetLayer(string layerPath)
    {
        if(mLayers.TryGetValue(layerPath,out GameObject layer))
        {
            return layer;
        }

        return null;
    }

    private void InitUILayers()
    {
         RegisterLayer(UIPathDef.UI_LAYER_BOTTOM_STATIC);
         RegisterLayer(UIPathDef.UI_LAYER_BOTTOM_DYNAMIC);
         RegisterLayer(UIPathDef.UI_LAYER_NORMAL_STATIC);
         RegisterLayer(UIPathDef.UI_LAYER_NORMAL_DYNAMIC);
         RegisterLayer(UIPathDef.UI_LAYER_MSG_STATIC);
         RegisterLayer(UIPathDef.UI_LAYER_MSG_DYNAMIC);
         RegisterLayer(UIPathDef.UI_LAYER_TOP_STATIC);
         RegisterLayer(UIPathDef.UI_LAYER_TOP_DYNAMIC);
    }
    private UIManager()
    {
        mAllPanels = new List<UIPanel>();
        mPanelPool = new Dictionary<System.Type, UIObject>();
        mLayers = new Dictionary<string, GameObject>();
        InitUILayers();
    }

    private T GetPanel<T>() where T : UIObject,new()
    {
        UIObject panel = null;
        if(!mPanelPool.TryGetValue(typeof(T),out panel))
        {
            panel = new T();
        }
        return panel as T;
    }

    private void PushPanel<T>(UIObject panel)
    {
        if(!mPanelPool.ContainsKey(typeof(T)))
        {
            mPanelPool.Add(typeof(T), panel);
        }
    }

    public void OpenPanel<T>(object[] openArgs = null) where T:UIPanel,new()
    {
        T panel = GetPanel<T>();
        if(!panel.CheckArgs(openArgs))
            return;

        string panelPath = UIPathDef.GetUIPath<T>();
        string UILayerPath = panel.GetPanelLayerPath();
        if(string.IsNullOrEmpty(panelPath))
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel Failed, panel {0} does not registered!",typeof(T));
            return;
        }

        var layer = GetLayer(UILayerPath);
        GameObject panelGo = LoadPanel(panelPath, layer);
        if(panelGo != null)
        {
            panel.BindUIObjectNodes(panelGo);
        }

        mAllPanels.Add(panel);
        panel.OnOpen(openArgs);
    }


    public void AddControl<T>(GameObject parentNode, object[] openArgs = null) where T : UIObject, new()
    {
        if (parentNode == null)
        {
            Log.Error(ErrorLevel.Critical, "AddControl Error,parentNode is null !!!");
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
            Log.Error(ErrorLevel.Critical, "LoadPanel Error,panel path null or empty!");
            return null;
        }

        if(parentNode == null)
        {
            Log.Error(ErrorLevel.Critical, "LoadPanel Error,parentNode null!");
            return null;
        }

        GameObject panelGo = null;
        GameObject obj = ResourceMgr.Load<GameObject>(panelPath, "Load UIPanel");
        if (obj != null)
        {
            panelGo = GameObject.Instantiate(obj);
            if(parentNode != null)
            {
                panelGo.transform.SetParent(parentNode.transform,false);
            }
        }
        else
        {
            Log.Error(ErrorLevel.Critical, "LoadPanel Failed,panel go load failed,panelPath:{0}", panelPath);
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
            panel.ClearAll();
            panel.DestroyUIObject();
        }    

        mAllPanels.RemoveAt(targetIndex);
    }

    public void Update(float deltaTime)
    {
        for(int i = 0;i<mAllPanels.Count;i++)
        {
            UIObject panel = mAllPanels[i];
            if(panel != null)
            {
                panel.Update(deltaTime);
            }
        }
    }
}
