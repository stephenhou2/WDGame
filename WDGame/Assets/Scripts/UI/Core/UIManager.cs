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
    private Dictionary<string, GameObject> mLayers = new Dictionary<string, GameObject>();

    /// <summary>
    /// 所有已经打开的面板 
    /// 理论上一个面板同一时间应该只有一份
    /// 多个的可以考虑设计为UIControl
    /// </summary>
    private Dictionary<System.Type, UIPanel> mAllOpenPanels = new Dictionary<System.Type, UIPanel>();

    /// <summary>
    /// 所有要加载的control
    /// </summary>
    private SimpleQueue<UILoadAction> mToAddControls = new SimpleQueue<UILoadAction>();

    /// <summary>
    /// UI实体缓存池
    /// </summary>
    private Dictionary<System.Type, GamePool<UIEntity>> mUIEntityPools = new Dictionary<System.Type, GamePool<UIEntity>>();

    /// <summary>
    /// UI GameObject 缓存池
    /// </summary>
    private Dictionary<string, GameObjectPool> mUIObjectPool = new Dictionary<string, GameObjectPool>();

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

        uiEntity.BindAllUINodes(uiObj);
        if (layerGo != null)
        {
            uiObj.transform.SetParent(layerGo.transform, false);
        }
    }

    private T GetUIEntity<T>() where T:UIEntity,new()
    {
        T entity = null;

        GamePool<UIEntity> pool;
        if(mUIEntityPools.TryGetValue(typeof(T),out pool))
        {
            if(pool != null)
            {
                entity = pool.PopObj() as T;
            }
        }

        if(entity == null)
        {
            entity = new T();
        }

        return entity;
    }

    private void PushUIEntity<T>(T entity) where T:UIEntity
    {
        GamePool<UIEntity> pool;

        System.Type type = entity.GetType();
        if (mUIEntityPools.TryGetValue(type, out pool))
        {
            if(pool == null)
            {
                pool = new GamePool<UIEntity>();
            }
        }
        else
        {
            pool = new GamePool<UIEntity>();
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
        System.Type type = panel.GetType();
        if(!mAllOpenPanels.ContainsKey(type))
        {
            mAllOpenPanels.Add(type, panel);
        }
    }

    private void OnUIControlLoadFinish(UIEntity holder, UIEntity entity, GameObject uiObj, string uiPath, GameObject layerGo, UILoadFinishCall call)
    {
        WillOpenUI(holder, entity, uiObj, uiPath, layerGo, call);
    }

    private GameObject GetUIObject(string uiPath)
    {
        GameObjectPool pool;
        if(!mUIObjectPool.TryGetValue(uiPath, out pool))
        {
            return null;
        }

        return pool.PopObj();
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

        GameObjectPool pool;

        if (!mUIObjectPool.TryGetValue(uiPath, out pool))
        {
            pool = new GameObjectPool(mUIPoolNode);
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
                   
                uiObj = InstantiateAndSetName(template);
                OnUIPanelLoadFinish(action.holder, panel, uiObj, action.uiPath, action.parent, action.call);
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Panel");
            if (template == null)
                return;

            uiObj = InstantiateAndSetName(template);
            OnUIPanelLoadFinish(action.holder, panel, uiObj, action.uiPath, action.parent, action.call);
        }
    }

    /// <summary>
    /// 目前仅做每帧加载上限限制，后面根据需求来处理（如还有面板在关闭动画过程中，需要等待等情况）
    /// </summary>
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

    /// <summary>
    /// 目前先一次性全关，后面根据需求来处理
    /// </summary>
    /// <param name="panel"></param>
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

                uiObj = InstantiateAndSetName(template);
                OnUIControlLoadFinish(action.holder,ctl, uiObj, action.uiPath, action.parent, action.call);
            });
        }
        else
        {
            GameObject template = ResourceMgr.Load<GameObject>(action.uiPath, "Load UI Control");
            if (template == null)
                return;

            uiObj = InstantiateAndSetName(template);
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

    private GameObject InstantiateAndSetName(GameObject template)
    {
        GameObject uiObj = GameObject.Instantiate(template);
        uiObj.name = template.name;
        return uiObj;
    }

    /// <summary>
    /// 检查面板是否打开
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool CheckPanelOpen<T>() where T:UIPanel
    {
        return mAllOpenPanels.ContainsKey(typeof(T));
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="openArgs"></param>
    public void OpenPanel<T>(string panelPath,UILoadFinishCall call = null,bool isAsync = true) where T:UIPanel,new()
    {
        if(CheckPanelOpen<T>())
        {
            Log.Error(ErrorLevel.Hint, "Reopen panel {0}", typeof(T));
            return;
        }

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
    /// 关闭面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="panel"></param>
    public void ClosePanel<T>()
    {
        UIPanel panel;
        if(!mAllOpenPanels.TryGetValue(typeof(T),out panel))
        {
            Log.Error(ErrorLevel.Critical, "ClosePanel Error, there is no open panel of {0}", typeof(T));
            return;
        }

        mToClosePanels.Enqueue(panel);
    }


    /// <summary>
    /// 直接绑定control
    /// 非加载式，直接绑定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="holder"></param>
    /// <param name="rootNode"></param>
    /// <param name="uiPath"></param>
    /// <returns></returns>
    public T BindControl<T>(UIEntity holder,GameObject rootNode,string uiPath) where T:UIControl,new()
    {
        if (holder == null)
        {
            Log.Error(ErrorLevel.Critical, "BindControl Failed,holder is null!");
            return null;
        }

        if(rootNode == null)
        {
            Log.Error(ErrorLevel.Critical, "BindControl Failed,rootNode is null!");
            return null;
        }

        var ctl = GetUIEntity<T>();
        ctl.BindAllUINodes(rootNode);
        // OnOpen
        ctl.UIEntityOnOpen(uiPath, holder);
        return ctl;
    }

    /// <summary>
    /// 添加control
    /// 资源加载+绑定control
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="holder">control 的持有者，可以是Panel，也可以是另一个Control，但不能是自己</param>
    /// <param name="uiPath">control 的资源路径</param>
    /// <param name="parent">挂载在哪个父物体下</param>
    /// <param name="call">加载结束后的回调</param>
    /// <param name="isAsync">是否开启异步加载</param>
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

    /// <summary>
    /// 移除Control
    /// </summary>
    /// <param name="holder">control的持有者</param>
    /// <param name="ctl">被移除的control</param>
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

        if(holder.GetHashCode() == ctl.GetHashCode())
        {
            Log.Error(ErrorLevel.Fatal, "RemoveControl Fatal Error, {0} holder is self! ",ctl.GetType());
            return;
        }

        ctl.UIEntityOnClose();
        holder.RemoveChildUIEntity(ctl);
    }

    /// <summary>
    /// 所有打开的面板Update
    /// </summary>
    /// <param name="deltaTime"></param>
    private void UpdateAllOpenPanels(float deltaTime)
    {
        foreach (var kv in mAllOpenPanels)
        {
            UIPanel panel = kv.Value;
            if(panel != null)
            {
                panel.Update(deltaTime);
            }
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

    /// <summary>
    /// 删除UIEntity 和 UI GameObject
    /// 移除Entity    （根据复用策略，可以进缓存池）
    /// 移除UI GameObject （根据复用策略，可以选择进缓存池）
    /// </summary>
    /// <param name="entity"></param>
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
