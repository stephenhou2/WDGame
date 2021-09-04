using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{

    /// <summary>
    /// 所有要打开的面板，都先加到队列中
    /// </summary>
    private SimpleQueue<UILoadAction> mToOpenPanels;

    /// <summary>
    /// 所有要关闭的面板，都先加到队列中
    /// </summary>
    private SimpleQueue<UIPanel> mToClosePanels;

    /// <summary>
    /// 所有UI挂载的layer root
    /// </summary>
    Dictionary<string, GameObject> mLayers;

    /// <summary>
    /// 所有已经打开的面板 
    /// </summary>
    private Dictionary<System.Type, UIPanel> mAllOpenPanels;

    // TODO:UI gameobject 复用
    //  难点：怎么在复用的时候将所有节点还原回原始状态


    /// <summary>
    /// 所有要加载的control
    /// </summary>
    private SimpleQueue<UILoadAction> mToAddControls;

    private Dictionary<int,UIControl> mAllControls;

    /// <summary>
    /// 注册UI面板的挂载层
    /// </summary>
    /// <param name="layerPath"></param>
    private void RegisterLayer(string layerPath)
    {
        if (string.IsNullOrEmpty(layerPath))
            return;

        GameObject layer = GameObject.Find(layerPath);
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
        mLayers = new Dictionary<string, GameObject>();
        mToOpenPanels = new SimpleQueue<UILoadAction>();
        mToClosePanels = new SimpleQueue<UIPanel>();
        mAllOpenPanels = new Dictionary<System.Type, UIPanel>();

        mToAddControls = new SimpleQueue<UILoadAction>();
        mAllControls = new Dictionary<int, UIControl>();

        InitUILayers();
    }

    private void InstantiateUI(UIObject uiObj, GameObject template, GameObject layerGo)
    {
        if(template == null)
        {
            Log.Error(ErrorLevel.Critical, "InstantiateUI Failed, type:{0}",uiObj.GetType());
            return;
        }

        GameObject uiGo = GameObject.Instantiate(template);
        uiObj.BindUIObjectNodes(uiGo);
        if (layerGo != null)
        {
            uiGo.transform.SetParent(layerGo.transform, false);
        }
    }

    private void OnUIPanelLoadFinish(UIPanel panel,GameObject template,GameObject layerGo,UILoadFinishCall call)
    {
        // Instantiate
        InstantiateUI(panel, template, layerGo);

        // On Load Finish
        if (call != null)
        {
            call(panel);
        }
        // OnOpen
        panel.UIObjectOnOpen(null);

        // Record Panel
        if (!mAllOpenPanels.ContainsKey(panel.GetType()))
        {
            mAllOpenPanels.Add(panel.GetType(), panel);
        }
    }

    private void ExcutePanelLoadAction(UILoadAction action)
    {
        UIPanel panel = action.uiObj as UIPanel;
        if(panel == null)
        {
            Log.Error(ErrorLevel.Normal, "ExcutePanelLoadAction Error,Load null UIPanel!");
        }

        if (action.isAsync)
        {
            ResourceMgr.AsyncLoadRes<GameObject> (action.uiPath, "Load UI Panel", (Object obj) =>
            {
                if(obj != null)
                {
                    GameObject template = obj as GameObject;
                    OnUIPanelLoadFinish(panel, template, action.parent, action.call);
                }
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Panel");
            OnUIPanelLoadFinish(panel, template,action.parent, action.call);
        }
    }

    private void _OpenPanels()
    {
        int cnt = Mathf.Min(UIDefine.Panel_Load_Per_Frame, mToOpenPanels.Count);
        for (int i = 0; i < cnt; i++)
        {
            UILoadAction action = mToOpenPanels.Dequeue();
            if (action != null)
            {
                ExcutePanelLoadAction(action);
            }
        }
    }

    private void ExcutePanelCloseAction(UIPanel panel)
    {
        if(panel != null)
        {
            panel.UIObjectOnClose();

            if (mAllOpenPanels.ContainsKey(panel.GetType()))
                mAllOpenPanels.Remove(panel.GetType());
        }
    }

    private void _ClosePanels()
    {
        while (mToClosePanels.HasItem())
        {
            UIPanel panel = mToClosePanels.Dequeue();

            if (panel != null)
            {
                ExcutePanelCloseAction(panel);
            }
        }
    }

    private void OnUIControlLoadFinish(UIObject holder,UIControl ctl, GameObject template, GameObject parent, UILoadFinishCall call)
    {
        InstantiateUI(ctl, template, parent);

        ctl.UIObjectOnOpen(holder);

        if (call != null)
        {
            call(ctl);
        }

        int key = ctl.GetHashCode();
        if (!mAllControls.ContainsKey(key))
        {
            mAllControls.Add(key,ctl);
        }
    }

    private void ExcuteControlLoadAction(UILoadAction action)
    {
        UIControl ctl = action.uiObj as UIControl;
        if (ctl == null)
        {
            Log.Error(ErrorLevel.Normal, "ExcuteControlLoadAction Error,Load null UIControl!");
        }

        if (action.isAsync)
        {
            ResourceMgr.AsyncLoadRes<GameObject>(action.uiPath, "Load UI Control", (Object obj) =>
            {
                if (obj != null)
                {
                    GameObject template = obj as GameObject;
                    OnUIControlLoadFinish(action.holder,ctl, template, action.parent, action.call);
                }
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Panel");
            OnUIControlLoadFinish(action.holder, ctl, template, action.parent, action.call);
        }
    }

    private void _AddControls()
    {
        int cnt = Mathf.Min(UIDefine.Control_Load_Per_Frame, mToAddControls.Count);
        for (int i = 0; i < cnt; i++)
        {
            UILoadAction action = mToAddControls.Dequeue();
            if (action != null)
            {
                ExcuteControlLoadAction(action);
            }
        }
    }


    /// <summary>
    /// open panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="openArgs"></param>
    public void OpenPanel<T>(UILoadFinishCall call = null,bool isAsync = true) where T:UIPanel,new()
    {
        T panel = new T();

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

        UILoadAction action = new UILoadAction(null,panel, panelPath, layer, call, isAsync);
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
        if (!mAllOpenPanels.TryGetValue(typeof(T), out panel))
        {
            Log.Error(ErrorLevel.Normal, "ClosePanel Error,close not opened panel,panel Type:{0}", typeof(T));
            return;
        }

        mToClosePanels.Enqueue(panel);
    }

    public void AddControl<T>(UIObject holder,string uiPath, GameObject parent, UILoadFinishCall call = null, bool isAsync = true)
        where T:UIControl,new()
    {
        if(holder == null)
        {
            Log.Error(ErrorLevel.Critical, "AddControl Failed,UI Control must has a holder!");
            return;
        }

        T control = new T();
        if (string.IsNullOrEmpty(uiPath))
        {
            Log.Error(ErrorLevel.Critical, "AddControl Failed, load {0} with empty path! ", typeof(T));
            return;
        }

        if (parent == null)
        {
            Log.Error(ErrorLevel.Critical, "AddControl Failed, load {0} with null parent", typeof(T));
            return;
        }

        UILoadAction action = new UILoadAction(holder,control, uiPath, parent, call, isAsync);
        mToAddControls.Enqueue(action);
    }

    public void RemoveControl(UIObject holder, UIControl ctl)
    {
        if (holder == null)
        {
            Log.Error(ErrorLevel.Critical, "RemoveControl Failed,holder is null!");
            return;
        }

        if (ctl == null)
        {
            Log.Error(ErrorLevel.Critical, "RemoveControl Failed,ctl is null!");
            return;
        }

        ctl.UIObjectOnClose();
        holder.RemoveChildUIObj(ctl);

        int key = ctl.GetHashCode();
        if (mAllControls.ContainsKey(key))
        {
            mAllControls.Remove(key);
        }
    }

    private void UpdateAllOpenPanels(float deltaTime)
    {
        foreach (var kv in mAllOpenPanels)
        {
            UIPanel panel = kv.Value;
            if (panel != null)
                panel.Update(deltaTime);
        }
    }   
    
    private void UpdateAllControls(float deltaTime)
    {
        foreach (var kv in mAllControls)
        {
            UIControl ctl = kv.Value;
            if (ctl != null)
            {
                ctl.Update(deltaTime);
            }
        }
    }

    // TBD: control 附属于
    // panel删除时要将子节点的control也删除（onRemove())
    // control 通过uimanager来删除

    // 考虑是否又复用的需求

    /// <summary>
    ///  UI Root Update...
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        _ClosePanels();        // close panels

        _OpenPanels();       // open panels

        _AddControls();      // add controls 

        UpdateAllOpenPanels(deltaTime);  // common panel update

        UpdateAllControls(deltaTime); // common control update
    }
}
