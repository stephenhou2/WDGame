using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract partial class UIObject
{
    private List<IEnumerator> mEnumerators; // 后面自己实现协程
    protected GameObject mRoot;
    private List<UnityEvent> mUIEvents;
    public List<UIObject> mChildUIObjs;
    /// <summary>
    /// control 持有者,可以是一个panel，也可以是另一个control
    /// </summary>
    protected UIObject mHolder;

    protected UIObject()
    {
        mEnumerators = new List<IEnumerator>();
        mUIEvents = new List<UnityEvent>();
        mChildUIObjs = new List<UIObject>();
    } // 构造函数

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

    public void SetHolder(UIObject holder)
    {
        mHolder = holder;
    }

    public void UIObjectOnClose()
    {
        DestroyAllChildUIObj();
        //if(mHolder !=  null)
        //{
        //    mHolder.RemoveChildUIObj(this);
        //}
        OnClose();
        DestroyUIObject();
        ClearAll();
    }

    public void UIObjectOnOpen(UIObject holder)
    {
        if(holder != null)
        {
            SetHolder(holder);
            holder.AddChildUIObj(this);
        }
        OnOpen();
    }

    public void AddChildUIObj(UIObject uiobj)
    {
        if(!mChildUIObjs.Contains(uiobj))
        {
            mChildUIObjs.Add(uiobj);
        }
    }

    public void DestroyAllChildUIObj()
    {
        foreach(UIObject uiObj in mChildUIObjs)
        {
            uiObj.UIObjectOnClose();
        }
    }

    public void RemoveChildUIObj(UIObject uiobj)
    {
        if (mChildUIObjs.Contains(uiobj))
        {
            mChildUIObjs.Remove(uiobj);
        }
    }

    public void ClearAll()
    {
        ClearUIEvents();
        CustomClear();
        mChildUIObjs.Clear();
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
    public virtual void CustomClear() { }

    public void DestroyUIObject()
    {
        if (mRoot != null)
            GameObject.Destroy(mRoot);
    }

    public void BindUIObjectNodes(GameObject root)
    {
        mRoot = root;
        if(mRoot != null)
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
        OnUpdate(deltaTime);
    }
}
