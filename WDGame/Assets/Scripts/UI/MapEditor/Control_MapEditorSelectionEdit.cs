using GameEngine;
using UnityEngine;

public class Control_MapEditorSelectionEdit : UIControl
{
    private Control_BrushEdit _Ctl_BrushEdit;

    public override void CustomClear()
    {
        _Ctl_BrushEdit = null;
    }

    protected override void BindUINodes()
    {
        BindControl<Control_BrushEdit>(ref _Ctl_BrushEdit, "Control_BrushEdit", string.Empty);
    }

    protected override void OnClose()
    {
        
    }

    protected override void OnOpen()
    {
        
    }
}
