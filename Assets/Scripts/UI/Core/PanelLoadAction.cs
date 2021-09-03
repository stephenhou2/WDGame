using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PanelLoadFinishCall(UIPanel panel);

public class PanelLoadAction
{
    public UIPanel panel;

    public PanelLoadFinishCall call;

    public bool isAsync;

    public PanelLoadAction(UIPanel panel, PanelLoadFinishCall call, bool isAsync)
    {
        this.panel = panel;
        this.call = call;
        this.isAsync = isAsync;
    }
}
