using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{

    /// <summary>
    /// 所有要打开的面板，都先加到队列中
    /// </summary>
    private GameQueue<PanelLoadAction> mToOpenPanels;

    /// <summary>
    /// 所有要关闭的面板，都先加到队列中
    /// </summary>
    private GameQueue<UIPanel> mToClosePanels;

    /// <summary>
    /// 所有UI挂载的layer root
    /// </summary>
    Dictionary<string, GameObject> mLayers;


    /// <summary>
    /// panel对象缓存池，面板关闭时，所用的UI脚本可以复用，减少GC
    /// </summary>
    private Dictionary<System.Type, UIPanel> mPanelPool;

    /// <summary>
    /// 所有已经打开的面板 
    /// </summary>
    private Dictionary<System.Type, UIPanel> mAllOpenPanels;

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
        foreach(string layer in UIPathDef.ALL_UI_LAYER)
        {
             RegisterLayer(layer);
        }
    }
    public UIManager()
    {
        mAllOpenPanels = new Dictionary<System.Type, UIPanel>();
        mPanelPool = new Dictionary<System.Type, UIPanel>();
        mLayers = new Dictionary<string, GameObject>();
        mToOpenPanels = new GameQueue<PanelLoadAction>();
        mToClosePanels = new GameQueue<UIPanel>();
        InitUILayers();
    }

    private void OnOpenPanel(UIPanel panel)
    {
        panel.OnOpen();

        if (!mAllOpenPanels.ContainsKey(panel.GetType()))
            mAllOpenPanels.Add(panel.GetType(), panel);
    }

    private void OnClosePanel(UIPanel panel)
    {
        panel.OnClose();
        panel.ClearAll();
        panel.DestroyUIObject();

        if(mAllOpenPanels.ContainsKey(panel.GetType()))
            mAllOpenPanels.Remove(panel.GetType());
    }

    private T PopPanel<T>() where T : UIPanel,new()
    {
        UIPanel panel = null;
        if (!mPanelPool.TryGetValue(typeof(T), out panel))
        {
            panel = new T();
        }
        else
        {
            mPanelPool.Remove(typeof(T));
        }

        return panel as T;
    }

    private void PushPanel(UIPanel panel)
    {
        if (panel == null)
        {
            Log.Error(ErrorLevel.Critical,"Push Panel Failed,panel is null!");
            return;
        }

        if(!mPanelPool.ContainsKey(panel.GetType()))
        {
            mPanelPool.Add(panel.GetType(), panel);
        }
    }

    private void OnUIPanelLoadFinish(UIPanel panel,GameObject template,GameObject layerGo,PanelLoadFinishCall call)
    {
        if (template != null)
        {
            GameObject panelGo = GameObject.Instantiate(template);

            panel.BindUIObjectNodes(panelGo);
            if(layerGo != null)
            {
                panelGo.transform.SetParent(layerGo.transform,false);
            }
        }

        OnOpenPanel(panel);
        if (call != null)
        {
            call(panel);
        }
    }

    private void HandleToOpenPanels()
    {
        if (mToClosePanels.HasItem()) // 还有UI没有完成关闭动作
            return;

        while(mToOpenPanels.HasItem())
        {
            PanelLoadAction action  = mToOpenPanels.Dequeue();
            UIPanel panel = action.panel;
            if(panel == null)
            {
                Log.Error(ErrorLevel.Normal, "HandleToOpenPanels Error,panel is null in to open queue!");
                continue;
            }
            string layerPath = panel.GetPanelLayerPath();
            string pathResPath = panel.GetPanelResPath();
            GameObject layer = GetLayer(panel.GetPanelLayerPath());
            if (layer == null)
            {
                Log.Error(ErrorLevel.Critical, "HandleToOpenPanels Failed, panel layer not find,UILayerPath:{0}", panel.GetPanelLayerPath());
                return;
            }

            if (action.isAsync)
            {
                ResourceMgr.AsyncLoadRes<GameObject> (pathResPath, "Load UI Panel", (Object obj) =>
                {
                    GameObject template = obj as GameObject;
                    OnUIPanelLoadFinish(panel, template, layer, action.call);
                });
            }
            else
            {
                GameObject template = ResourceMgr.Load<GameObject>(pathResPath, "Load UI Panel");
                OnUIPanelLoadFinish(panel, template,layer, action.call);
            }
        }
    }

    private void HandleToClosePanels()
    {
        while (mToClosePanels.HasItem())
        {
            UIPanel panel = mToClosePanels.Dequeue();

            if(panel != null)
            {
                OnClosePanel(panel);
                PushPanel(panel);
            }
        }
    }

    /// <summary>
    /// open panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="openArgs"></param>
    public void OpenPanel<T>(PanelLoadFinishCall call = null,bool isAsync = true) where T:UIPanel,new()
    {
        T panel = PopPanel<T>(); // 这里获得的panel一定不会为空

        string UILayerPath = panel.GetPanelLayerPath();
        string panelPath = panel.GetPanelResPath();

        if (string.IsNullOrEmpty(panelPath))
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel Failed, panel {0} does not registered!", typeof(T));
            return;
        }

        GameObject layer = GetLayer(UILayerPath);
        if (layer == null)
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel Failed, panel layer not find,UILayerPath:{0}", UILayerPath);
            return;
        }

        PanelLoadAction action = new PanelLoadAction(panel, call, isAsync);
        mToOpenPanels.Enqueue(action);
    }

    /// <summary>
    /// close panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="panel"></param>
    public void ClosePanel<T>()
    {
        UIPanel panel;
        if (mAllOpenPanels.TryGetValue(typeof(T),out panel))
        {
            Log.Error(ErrorLevel.Normal, "ClosePanel Error,close not opened panel,panel Type:{0}", typeof(T));
            return;
        }

        mToClosePanels.Enqueue(panel);
    }


    /// <summary>
    ///  UI Root Update...
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        HandleToClosePanels();
        HandleToOpenPanels();

        foreach(var kv in mAllOpenPanels)
        {
            UIPanel panel = kv.Value;
            if (panel != null)
                panel.Update(deltaTime);
        }
    }
}
