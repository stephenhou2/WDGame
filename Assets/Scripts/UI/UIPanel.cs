using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIPanel
{
    protected abstract void BindUINodes();
    public abstract void OnOpen(object[] openArgs);
    public abstract void OnClose();

    private List<IEnumerator> mEnumerators = new List<IEnumerator>(); // 后面自己实现协程
    protected GameObject mPanelRoot;
    private Dictionary<string, GameObject> mNodes;

    protected UIPanel() { }

    public void BindPanelRootNode(GameObject panelRoot)
    {
        mPanelRoot = panelRoot;
    }

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

    protected int BindNode(ref GameObject go,string node)
    {
        if (mPanelRoot == null)
        {
            Log.Error("BindNode failed,panel root node is null!");
            return -1;
        }

        Transform targetNode = UIInterface.FindChildNode(mPanelRoot.transform, node);
        if (targetNode == null)
        {
            Log.Error("BindNode failed,does not has target node,node={0}",node);
            return -2;
        }
        go = targetNode.gameObject;
        return 0;
    }

    protected int BindButtonNode(ref GameObject go, string node, UnityAction call)
    {
        if (call == null)
            return -3;

        int ret = BindNode(ref go, node);
        if (ret < 0)
            return ret;

        Button btn = go.GetComponent<Button>();
        if (btn == null)
            return -4;

        btn.onClick.AddListener(call);
        return 0;
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
