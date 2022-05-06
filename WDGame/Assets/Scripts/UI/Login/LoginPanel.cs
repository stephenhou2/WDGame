using UnityEngine;
using GameEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoginPanel : UIPanel
{
    private Button Button_EnterMapEditor;
    private Button Button_EnterPlotEditor;

    public override void CustomClear()
    {
        
    }

    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_TOP_STATIC;
    }

    private void OnClickEnterMapEditor()
    {
        EmitterBus.Fire(ModuleDef.SceneModule, "SwitchToScene", new GameEventArgs_String(SceneDef.MapEditorScene));
    }

    private void OnClickEnterPlotEditor()
    {
        EmitterBus.Fire(ModuleDef.SceneModule, "SwitchToScene", new GameEventArgs_String(SceneDef.PlotEditorScene));
    }

    protected override void BindUINodes()
    {
        BindButtonNode(ref Button_EnterMapEditor, "Button_EnterMapEditor", OnClickEnterMapEditor);
        BindButtonNode(ref Button_EnterPlotEditor, "Button_EnterPlotEditor", OnClickEnterPlotEditor);
    }



    protected override void OnClose()
    {
        
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string,object> openArgs)
    {
        
    }
}
