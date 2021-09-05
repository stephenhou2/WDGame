using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract partial class UIEntity
{
    private List<IEnumerator> mEnumerators = new List<IEnumerator>(); // 后面自己实现协程
    private List<UnityEvent> mUIEvents = new List<UnityEvent>();
    public List<UIEntity> mChildUIEntitys = new List<UIEntity>();
    protected GameObject mUIRoot;
    /// <summary>
    /// control 持有者,可以是一个panel，也可以是另一个control
    /// </summary>
    protected UIEntity mHolder;
    private string mUIPath;

    /// <summary>
    /// 复用策略
    /// </summary>
    /// <returns></returns>
    public virtual int GetRecycleStrategy()
    {
        return UIDefine.UI_Recycle_DontRecycle;
    }

    protected UIEntity(){} // 构造函数

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected abstract void BindUINodes();

    /// <summary>
    /// panel 打开时调用
    /// </summary>
    protected abstract void OnOpen();

    /// <summary>
    /// panel关闭前调用
    /// </summary>
    protected abstract void OnClose();

    public string GetUIResPath()
    {
        return mUIPath;
    }

    public void SetHolder(UIEntity holder)
    {
        mHolder = holder;
    }

    public UIEntity GetHolder()
    {
        return mHolder;
    }

    public GameObject GetRootObj()
    {
        return mUIRoot;
    }

    public void UIEntityOnClose()
    {
        DestroyAllChildUIEntity();
        //if(mHolder !=  null)
        //{
            //mHolder.RemoveChildUIObj(this);
        //}
        OnClose();
        DestroyUIEntity();
        ClearAll();
    }

    public void UIEntityOnOpen(string uiPath,UIEntity holder)
    {
        mUIPath = uiPath;

        if (holder != null)
        {
            SetHolder(holder);
            holder.AddChildUIEntity(this);
        }
        OnOpen();
    }

    public void AddChildUIEntity(UIEntity uiEntity)
    {
        if(!mChildUIEntitys.Contains(uiEntity))
        {
            mChildUIEntitys.Add(uiEntity);
        }
    }

    public void DestroyAllChildUIEntity()
    {
        foreach(UIEntity uiEntity in mChildUIEntitys)
        {
            uiEntity.UIEntityOnClose();
        }
    }

    public void RemoveChildUIEntity(UIEntity uiEntity)
    {
        if (mChildUIEntitys.Contains(uiEntity))
        {
            mChildUIEntitys.Remove(uiEntity);
        }
    }

    public void ClearAll()
    {
        ClearUIEvents();
        CustomClear();
        mChildUIEntitys.Clear();
        mHolder = null;
    }

    protected void ClearUIEvents()
    {
        foreach (var evt in mUIEvents)
        {
            evt.RemoveAllListeners();
        }
    }

    /// <summary>
    /// 清除无用缓存和失效的事件监听
    /// </summary>
    public abstract void CustomClear();

    public bool CheckRecycleUIEntity()
    {
        return (GetRecycleStrategy() & UIDefine.UI_Recycle_UIEntity) > 0;
    }

    public bool CheckRecycleUIGameObject()
    {
        return (GetRecycleStrategy() & UIDefine.UI_Recycle_UIGameObject) > 0;
    }

    public void DestroyUIEntity()
    {
        UIManager.Ins.DestroyUIEntity(this);
    }

    public void BindUINodes(GameObject root)
    {
        mUIRoot = root;
        if(mUIRoot != null)
        {
            BindUINodes();
        }
        else
        {
            Log.Error(ErrorLevel.Critical, "BindUIObjectNodes Error, root is null !!!");
        }
    }

    protected void StartCoroutine(IEnumerator coroutine)
    {
        
    }

    protected void StopCoroutine(IEnumerator coroutine)
    {

    }

    protected virtual void OnUpdate(float deltaTime) { }

    public void Update(float deltaTime)
    {
        foreach(UIEntity childEntity in mChildUIEntitys)
        {
            if(childEntity != null)
            {
                childEntity.Update(deltaTime);
            }
        }

        OnUpdate(deltaTime);
    }
}
