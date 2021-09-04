using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UILoadFinishCall(UIObject ui);

public class UILoadAction
{
    public UIObject holder;
    public UIObject uiObj;
    public string uiPath;
    public GameObject parent;
    public UILoadFinishCall call;
    public bool isAsync;

    public UILoadAction(UIObject holder,UIObject uiObj, string uiPath, GameObject parent, UILoadFinishCall call, bool isAsync)
    {
        this.holder = holder;
        this.uiObj = uiObj;
        this.uiPath = uiPath;
        this.parent = parent;
        this.call = call;
        this.isAsync = isAsync;
    }
}
