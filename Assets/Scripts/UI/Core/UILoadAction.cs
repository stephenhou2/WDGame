using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UILoadFinishCall(UIEntity ui);

public class UILoadAction
{
    public UIEntity holder;
    public UIEntity uiEntity;
    public string uiPath;
    public GameObject parent;
    public UILoadFinishCall call;
    public bool isAsync;

    public UILoadAction(UIEntity holder,UIEntity uiEntity, string uiPath, GameObject parent, UILoadFinishCall call, bool isAsync)
    {
        this.holder = holder;
        this.uiEntity = uiEntity;
        this.uiPath = uiPath;
        this.parent = parent;
        this.call = call;
        this.isAsync = isAsync;
    }
}
