using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{
    private List<UIPanel> mAllPanels;
    /// <summary>
    /// 所有要打开的面板，都先加到队列中
    /// </summary>
    private GameQueue<UIPanelOpenData> mToOpenPanels;

    /// <summary>
    /// 所有要关闭的面板，都先加到队列中
    /// </summary>
    private GameQueue<UIPanel> mToClosePanels;

    Dictionary<string, GameObject> mLayers;


    /// <summary>
    /// panel对象缓存池，面板关闭时，所用的UI脚本可以复用，减少GC
    /// </summary>
    private Dictionary<System.Type, UIObject> mPanelPool;

    /// TODO:UI gameobject 复用
    ///  难点：怎么在复用的时候将所有节点还原回原始状态

    /// <summary>
    /// 注册UI面板的挂载层
    /// </summary>
    /// <param name="layerPath"></param>
    private void RegisterLayer(string layerPath)
    {
        if (string.IsNullOrEmpty(layerPath))
            return;

        var layer = GameObject.Find(layerPath);
        if (layer != null)
            mLayers.Add(layerPath, layer);
    }

    /// <summary>
    ///  获取UI面板的挂在层
    /// </summary>
    /// <param name="layerPath"></param>
    /// <returns></returns>
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
        mToOpenPanels = new GameQueue<UIPanelOpenData>();
        mToClosePanels = new GameQueue<UIPanel>();
        InitUILayers();
    }

    private T GetPanel<T>() where T : UIPanel,new()
    {
        UIObject panel = null;
        if(!mPanelPool.TryGetValue(typeof(T),out panel))
        {
            panel = new T();
        }
        return panel as T;
    }

    private void PushPanel(UIPanel panel)
    {
        if(!mPanelPool.ContainsKey(typeof(T)))
        {
            mPanelPool.Add(typeof(T), panel);
        }
    }

    private void HandleToOpenPanels()
    {
        if (mToClosePanels.HasItem()) // 还有UI没有完成关闭动作
            return;

        while(mToOpenPanels.HasItem())
        {
            UIPanelOpenData panelData  = mToOpenPanels.Dequeue();
            if(panelData != null)
            {
                GameObject panelGo = LoadPanel(panelData.panelPath, panelData.layer);
                UIPanel panel = panelData.panel;
                if (panelGo != null)
                {
                    panel.BindUIObjectNodes(panelGo);
                }

                mAllPanels.Add(panelData.panel);
                panel.OnOpen(panelData.openArgs);
            }
        }
    }

    private void _ClosePanel(UIPanel panel)
    {
        int targetIndex = -1;
        for (int i = 0; i < mAllPanels.Count; i++)
        {
            if (mAllPanels[i].GetType().Equals(panel.GetType()))
            {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1)
        {
            Log.Error(ErrorLevel.Critical, "ClosePanel Error,close panel not in panel list,panel:{0}", panel.ToString());
            return;
        }

        panel.OnClose();
        panel.ClearAll();
        panel.DestroyUIObject();
        PushPanel(panel);
        mAllPanels.RemoveAt(targetIndex);
    }

    private void HandleToClosePanels()
    {
        while (mToClosePanels.HasItem())
        {
            UIPanel panel = mToClosePanels.Dequeue();

            if(panel != null)
            {
                _ClosePanel(panel);
            }
        }
    }

    /// <summary>
    /// open panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="openArgs"></param>
    public void OpenPanel<T>(object[] openArgs = null) where T:UIPanel
    {
        T panel = GetPanel<T>(); // 这里获得的panel一定不会为空
        if(!panel.CheckArgs(openArgs))
            return;

        string panelPath = UIPathDef.GetUIPath<T>();
        string UILayerPath = panel.GetPanelLayerPath();
        if(string.IsNullOrEmpty(panelPath))
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel Failed, panel {0} does not registered!",typeof(T));
            return;
        }

        GameObject layer = GetLayer(UILayerPath);
        if (layer == null)
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel Failed, panel layer not find,UILayerPath:{0}", UILayerPath);
            return;
        }

        UIPanelOpenData data = new UIPanelOpenData(panel, panelPath, layer, openArgs);
        mToOpenPanels.Enqueue(data);
    }

    /// <summary>
    /// close panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="panel"></param>
    public void ClosePanel<T>(T panel) where T:UIPanel
    {
        if (panel == null)
        {
            Log.Error(ErrorLevel.Critical, "ClosePanel Error,panel is null!");
            return;
        }

        if (mAllPanels.Count == 0)
        {
            Log.Error(ErrorLevel.Critical, "ClosePanel Error,close panel when there is no panel in panel list", panel.ToString());
            return;
        }

        mToClosePanels.Enqueue(panel);
    }

    /// <summary>
    /// 加载Panel go
    /// </summary>
    /// <param name="panelPath"></param>
    /// <param name="parentNode"></param>
    /// <returns></returns>
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

    public void AddControl<T>(GameObject parentNode, object[] openArgs = null) where T : UIObject, new()
    {
        if (parentNode == null)
        {
            Log.Error(ErrorLevel.Critical, "AddControl Error,parentNode is null !!!");
            return;
        }


    }

    /// <summary>
    ///  UI Root Update...
    /// </summary>
    /// <param name="deltaTime"></param>
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
