using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelOpenData 
{
    public UIPanel panel;
    public string panelPath;
    public GameObject layer;
    public object[] openArgs;

    public UIPanelOpenData(UIPanel panel, string panelPath, GameObject layer,object[] openArgs)
    {
        this.panel = panel;
        this.panelPath = panelPath;
        this.layer = layer;
        this.openArgs = openArgs;
    }
}
