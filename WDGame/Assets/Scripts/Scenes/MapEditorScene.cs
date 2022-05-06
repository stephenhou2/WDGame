using GameEngine;

public class MapEditorScene : IScene
{
    private GameMapEditor mMapEditor;

    public string GetSceneName()
    {
        return SceneDef.MapEditorScene;
    }

    public void OnSceneEnter()
    {
        mMapEditor = GameMapEditor.Ins;
        mMapEditor.OnSceneEnter();

        UIManager.Ins.OpenPanel<Panel_MapEditor>("UIPrefab/MapEditor/Panel_MapEditor", null);
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
        
    }

    public void OnSceneUpdate(float deltaTime)
    {
        mMapEditor.OnUpdate(deltaTime);
    }
}
