using UnityEngine;
using GameEngine;

public class LoginPanel : UIPanel
{
    private GameObject Button_EnterMapEditor;

    public override void CustomClear()
    {
        
    }

    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_TOP_STATIC;
    }

    private void OnClickEnterMapEditor()
    {
        EmitterBus.Fire(ModuleDef.SceneMgr, "SwitchToScene", new GameEventArgs_String(SceneDef.MapEditorScene));
    }

    protected override void BindUINodes()
    {
        BindButtonNode(ref Button_EnterMapEditor, "Button_EnterMapEditor", OnClickEnterMapEditor);
    }

    protected override void OnClose()
    {
        
    }

    protected override void OnOpen()
    {
        
    }
}
