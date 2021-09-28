using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorScene : IScene
{
    private GameMapEditor mMapEditor; 

    public void OnSceneEnter()
    {
        mMapEditor = GameMapEditor.Ins;
        mMapEditor.OnSceneEnter();

        UIManager.Ins.OpenPanel<Panel_MapEditor>("UI/MapEditor/Panel_MapEditor", null);
    }

    public void OnSceneExit()
    {
        if(mMapEditor != null)
        {
            mMapEditor.OnSceneExit();
        }
    }

    public void OnSceneLateUpdate(float deltaTime)
    {
        mMapEditor.OnUpdate(deltaTime);
    }

    public void OnSceneUpdate(float deltaTime)
    {
        
    }
}
