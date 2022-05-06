using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEngine;

public class PlotEditorScene : IScene
{
    public string GetSceneName()
    {
        return "PlotEditorScene";
    }

    public void OnSceneEnter()
    {
        Log.Logic(LogLevel.Hint, "On Enter Plot Edit Scene");
        UIManager.Ins.OpenPanel<PanelPlotEditor>("UIPrefab/PlotEditor/Panel_PlotEditor");
    }

    public void OnSceneExit()
    {
        Log.Logic(LogLevel.Hint, "On Exit Plot Edit Scene");
    }

    public void OnSceneLateUpdate(float deltaTime)
    {
    }

    public void OnSceneUpdate(float deltaTime)
    {
    }
}
