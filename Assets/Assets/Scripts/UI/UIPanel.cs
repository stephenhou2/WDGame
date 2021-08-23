using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel
{
    protected abstract void BindUINodes();
    public abstract void OnOpen(PanelOpenArgs openArgs);
    public abstract void OnClose();

    private List<IEnumerator> mEnumerators = new List<IEnumerator>(); // 后面自己实现协程
    protected GameObject mPanelRoot;

    public virtual void Awake() { }
    public virtual void Start() { }

    //public UIPanel(GameObject panelGo)
    //{
    //    //mEnumerators = new List<IEnumerator>();
    //    mPanelRoot = panelGo;
    //    if(mPanelRoot != null)
    //    {
    //        BindUINodes();
    //    }
    //}

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
