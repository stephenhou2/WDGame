using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract partial class UIPanel
{

    private List<IEnumerator> mEnumerators; // 后面自己实现协程
    protected GameObject mPanelRoot;
    private List<UnityEvent> mUIEvents;

    protected UIPanel() 
    {
        mEnumerators = new List<IEnumerator>();
        mUIEvents = new List<UnityEvent>();
    } // 构造函数

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected abstract void BindUINodes();
    public abstract string GetPanelRootPath();

    /// <summary>
    /// panel 打开时调用
    /// </summary>
    /// <param name="openArgs"></param>
    public abstract void OnOpen(object[] openArgs);

    /// <summary>
    /// panel关闭前调用
    /// </summary>
    public abstract void OnClose();

    /// <summary>
    /// panel 参数检查
    /// </summary>
    /// <param name="openArgs"></param>
    /// <returns></returns>
    public abstract bool CheckOpenArgs(object[] openArgs);

    ///// <summary>
    ///// UI类型
    ///// </summary>
    ///// <returns></returns>
    //public abstract int GetUIType();

    /// <summary>
    /// close时,panel对象会被放入缓存池中复用，这里用来清理原来面板里的无用缓存
    /// </summary>
    public virtual void Clear()
    {
        foreach(var evt in mUIEvents)
        {
            evt.RemoveAllListeners();
        }
    }
    

    public void DestroyPanelObj()
    {
        if (mPanelRoot != null)
            GameObject.Destroy(mPanelRoot);
    }


    protected void Close<T>(T panel)
    {
        UIManager.Ins.ClosePanel<T>(panel);
    }

    public void BindPanelNodes(GameObject panelRoot)
    {
        mPanelRoot = panelRoot;
        if(mPanelRoot != null)
        {
            BindUINodes();
        }
        else
        {
            Log.Error("BindPanelNodes Error,panel root is null !!!");
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
