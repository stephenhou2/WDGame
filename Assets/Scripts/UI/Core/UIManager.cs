using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{
    /// <summary>
    /// 所有要打开的面板，都先加到队列中
    /// </summary>
    private SimpleQueue<UILoadAction> mToOpenPanels = new SimpleQueue<UILoadAction>();

    /// <summary>
    /// 所有要关闭的面板，都先加到队列中
    /// </summary>
    private SimpleQueue<UIPanel> mToClosePanels = new SimpleQueue<UIPanel>();

    /// <summary>
    /// 所有UI挂载的layer root
    /// </summary>
    Dictionary<string, GameObject> mLayers = new Dictionary<string, GameObject>();

    /// <summary>
    /// 所有已经打开的面板 
    /// </summary>
    private Dictionary<System.Type, UIPanel> mAllOpenPanels = new Dictionary<System.Type, UIPanel>();

    /// <summary>
    /// 所有要加载的control
    /// </summary>
    private SimpleQueue<UILoadAction> mToAddControls = new SimpleQueue<UILoadAction>();

    /// <summary>
    /// UI实体缓存池
    /// </summary>
    private Dictionary<System.Type, GamePool> mUIEntityPools = new Dictionary<System.Type, GamePool>();

    /// <summary>
    /// UI GameObject 缓存池
    /// </summary>
    private Dictionary<string,GamePool> mUIObjectPool = new Dictionary<string, GamePool>();

    /// <summary>
    ///  UI GameObject 缓存池在场景中的挂点
    /// </summary>
    private GameObject mUIPoolNode;

    /// <summary>
    /// 注册UI面板的挂载层
    /// </summary>
    /// <param name="layerPath"></param>
    private void RegisterLayer(GameObject uiRoot,string layerPath)
    {
        if (string.IsNullOrEmpty(layerPath))
            return;

        Transform layer = uiRoot.transform.Find(layerPath);
        if (layer != null)
            mLayers.Add(layerPath, layer.gameObject);
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

    private void InitUINodes()
    {
        GameObject UIRoot = GameObject.Find(UIPathDef.UI_ROOT);
        if(UIRoot == null)
        {
            Log.Error(ErrorLevel.Fatal, "UI Root Not Find");
            return;
        }

        mUIPoolNode = UIRoot.transform.Find(UIPathDef.UI_POOL_NODE).gameObject;

        // 绑定所有UI层级
        foreach(string layer in UIPathDef.ALL_UI_LAYER)
        {
             RegisterLayer(UIRoot,layer);
        }
    }

    public UIManager()
    {
        InitUINodes();
    }

    private void EntityBindUIObj(UIEntity uiEntity, GameObject uiObj, GameObject layerGo)
    {
        if(uiObj == null)
        {
            Log.Error(ErrorLevel.Critical, "EntityBindUIObj Failed, type:{0}", uiEntity.GetType());
            return;
        }

        uiEntity.BindUINodes(uiObj);
        if (layerGo != null)
        {
            uiObj.transform.SetParent(layerGo.transform, false);
        }
    }

    private T GetUIEntity<T>() where T:UIEntity,new()
    {
        T entity = null;

        GamePool pool;
        if(mUIEntityPools.TryGetValue(typeof(T),out pool))
        {
            if(pool != null)
            {
                entity = pool.PopObj<T>();
            }
        }

        if(entity == null)
        {
            entity = new T();
        }

        return entity;
    }

    public void PushUIEntity<T>(T entity) where T:UIEntity
    {
        GamePool pool;

        System.Type type = entity.GetType();
        if (mUIEntityPools.TryGetValue(type, out pool))
        {
            if(pool == null)
            {
                pool = new GamePool(type);
            }
        }
        else
        {
            pool = new GamePool(type);
            mUIEntityPools.Add(type, pool);
        }

        pool.PushObj(entity);
    }


    private void WillOpenUI(UIEntity holder, UIEntity entity, GameObject uiObj, string uiPath, GameObject layerGo, UILoadFinishCall call)
    {
        EntityBindUIObj(entity, uiObj, layerGo);

        // On Load Finish
        if (call != null)
        {
            call(entity);
        }

        // OnOpen
        entity.UIEntityOnOpen(uiPath, holder);
    }

    private void OnUIPanelLoadFinish(UIEntity holder,UIPanel panel,GameObject uiObj, string uiPath, GameObject layerGo,UILoadFinishCall call)
    {
        WillOpenUI(holder, panel, uiObj, uiPath, layerGo, call);

        // Record Panel
        if (!mAllOpenPanels.ContainsKey(panel.GetType()))
        {
            mAllOpenPanels.Add(panel.GetType(), panel);
        }
    }

    private void OnUIControlLoadFinish(UIEntity holder, UIEntity entity, GameObject uiObj, string uiPath, GameObject layerGo, UILoadFinishCall call)
    {
        WillOpenUI(holder, entity, uiObj, uiPath, layerGo, call);
    }

    private GameObject GetUIObject(string uiPath)
    {
        GamePool pool;
        if(!mUIObjectPool.TryGetValue(uiPath, out pool))
        {
            return null;
        }

        return pool.PopObj<GameObject>();
    }

    private void PushUIObject(string uiPath,GameObject uiObj)
    {
        if (string.IsNullOrEmpty(uiPath))
        {
            Log.Error(ErrorLevel.Normal, "PushUIObject Failed,empty uiPath");
            return;
        }

        if (uiObj == null)
        {
            Log.Error(ErrorLevel.Normal, "PushUIObject Failed,push null game object");
            return;
        }

        GamePool pool;
        if (!mUIObjectPool.TryGetValue(uiPath, out pool))
        {
            pool = new GamePool(typeof(GameObject));
            mUIObjectPool.Add(uiPath, pool);
        }

        pool.PushObj(uiObj);
        uiObj.transform.SetParent(mUIPoolNode.transform,false);
    }

    private void ExcutePanelLoadAction(UILoadAction action)
    {
        UIPanel panel = action.uiEntity as UIPanel;
        if(panel == null)
        {
            Log.Error(ErrorLevel.Normal, "ExcutePanelLoadAction Error,Load null UIPanel!");
        }

        GameObject uiObj = GetUIObject(action.uiPath);

        if (uiObj != null)
        {
            OnUIPanelLoadFinish(action.holder, panel, uiObj, action.uiPath, action.parent, action.call);
            return;
        }

        if (action.isAsync)
        {
            ResourceMgr.AsyncLoadRes<GameObject> (action.uiPath, "Load UI Panel", (Object obj) =>
            {
                if (obj == null)
                    return;

                GameObject template = obj as GameObject;
                if (template == null)
                    return;
                   
                uiObj = GameObject.Instantiate(template);
                OnUIPanelLoadFinish(action.holder, panel, uiObj, action.uiPath, action.parent, action.call);
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Panel");
            if (template == null)
                return;

            uiObj = GameObject.Instantiate(template);
            OnUIPanelLoadFinish(action.holder, panel, uiObj, action.uiPath, action.parent, action.call);
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
            panel.UIEntityOnClose();

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

    private void ExcuteControlLoadAction(UILoadAction action)
    {
        UIControl ctl = action.uiEntity as UIControl;
        if (ctl == null)
        {
            Log.Error(ErrorLevel.Normal, "ExcuteControlLoadAction Error,Load null UIControl!");
        }

        GameObject uiObj = GetUIObject(action.uiPath);
        
        if(uiObj != null)
        {
            OnUIControlLoadFinish(action.holder, ctl, uiObj, action.uiPath, action.parent, action.call);
            return;
        }

        if (action.isAsync)
        {
            ResourceMgr.AsyncLoadRes<GameObject>(action.uiPath, "Load UI Control", (Object obj) =>
            {
                if (obj == null)
                    return;
                
                GameObject template = obj as GameObject;
                if (template == null)
                    return;

                uiObj = GameObject.Instantiate(template);
                OnUIControlLoadFinish(action.holder,ctl, uiObj, action.uiPath, action.parent, action.call);
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Control");
            if (template == null)
                return;

            uiObj = GameObject.Instantiate(template);
            OnUIControlLoadFinish(action.holder, ctl, uiObj, action.uiPath, action.parent, action.call);
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
    public void OpenPanel<T>(string panelPath,UILoadFinishCall call = null,bool isAsync = true) where T:UIPanel,new()
    {
        T panel = GetUIEntity<T>();
        string UILayerPath = panel.GetPanelLayerPath();

        if (string.IsNullOrEmpty(panelPath))
        {
            Log.Error(ErrorLevel.Critical, "OpenPanel {0} Failed, empty panel path!",typeof(T));
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

    public void AddControl<T>(UIEntity holder,string uiPath, GameObject parent, UILoadFinishCall call = null, bool isAsync = true)
        where T:UIControl,new()
    {
        if(holder == null)
        {
            Log.Error(ErrorLevel.Critical, "AddControl Failed,UI Control must has a holder!");
            return;
        }

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

        T control = GetUIEntity<T>();
        UILoadAction action = new UILoadAction(holder, control, uiPath, parent, call, isAsync);
        mToAddControls.Enqueue(action);
    }

    public void RemoveControl(UIEntity holder, UIControl ctl)
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

        ctl.UIEntityOnClose();
        holder.RemoveChildUIEntity(ctl);
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
    }

    public void DestroyUIEntity(UIEntity entity)
    {
        if (entity == null)
            return;

        if (entity.CheckRecycleUIEntity()) // recycle ui entity
        {
            PushUIEntity(entity);
        }

        if (entity.CheckRecycleUIGameObject()) // recycle ui gameobject
        {
            PushUIObject(entity.GetUIResPath(),entity.GetRootObj());
        }
        else if (entity.GetRootObj() != null) // destroy ui gameobject,
        {
            GameObject.Destroy(entity.GetRootObj());
        }
    }
}
