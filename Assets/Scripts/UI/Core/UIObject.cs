using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract partial class UIObject
{
    private List<IEnumerator> mEnumerators; // 后面自己实现协程
    protected GameObject mRoot;
    private List<UnityEvent> mUIEvents;

    protected UIObject() 
    {
        mEnumerators = new List<IEnumerator>();
        mUIEvents = new List<UnityEvent>();
    } // 构造函数

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected abstract void BindUINodes();

    /// <summary>
    /// panel 参数检查
    /// </summary>
    /// <param name="openArgs"></param>
    /// <returns></returns>
    public abstract bool CheckArgs(object[] openArgs);

    public void ClearAll()
    {
        ClearUIEvents();
        CustomClear();
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
