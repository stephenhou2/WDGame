using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Panel_MapEditor : UIPanel
{
    private Control_MapEditorSelection _Ctl_Selection;
    private Control_MapEditorSelectionEdit _Ctl_SelectionEdit;


    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_BOTTOM_STATIC;
    }

    protected override void OnOpen()
    {

    }

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected override void BindUINodes()
    {
        BindControl<Control_MapEditorSelection>(ref _Ctl_Selection, "_Node_Selection", string.Empty);
        BindControl<Control_MapEditorSelectionEdit>(ref _Ctl_SelectionEdit, "_Node_Selection_Edit", string.Empty);
    }




    public override void CustomClear()
    {
        _Ctl_Selection = null;
        _Ctl_SelectionEdit = null;
    }

    protected override void OnClose()
    {

    }
}
