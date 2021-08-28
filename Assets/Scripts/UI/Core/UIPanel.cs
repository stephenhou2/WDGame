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

    //public void AddUIEvent(unti)

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected abstract void BindUINodes();

    /// <summary>
    /// panel 参数检查
    /// </summary>
    /// <param name="openArgs"></param>
    /// <returns></returns>
    public abstract bool CheckOpenArgs(object[] openArgs);

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

    public virtual void OnOpen(object[] openArgs)
    {
        if (mPanelRoot != null)
        {
            BindUINodes();
        }
    }

    public void DestroyPanelObj()
    {
        if (mPanelRoot != null)
            GameObject.Destroy(mPanelRoot);
    }

    public virtual void OnClose()
    {

    }
    protected void Close<T>(T panel)
    {
        UIManager.Ins.ClosePanel<T>(panel);
    }

    public void BindPanelRootNode(GameObject panelRoot)
    {
        mPanelRoot = panelRoot;
    }

    protected void StartCoroutine(IEnumerator coroutine)
    {
        
    }

    protected void StopCoroutine(IEnumerator coroutine)
    {

    }

    protected virtual void OnUpdate() { }

    public void Update()
    {
        OnUpdate();

    }
}
