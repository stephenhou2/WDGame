using GameEngine;

public class LoginScene : IScene
{
    public void OnSceneEnter()
    {
        UIManager.Ins.OpenPanel<LoginPanel>("UIPrefab/Login/Panel_Login",null);
    }

    public string GetSceneName()
    {
        return SceneDef.LoginScene;
    }

    public void OnSceneExit()
    {
        UIManager.Ins.ClosePanel<LoginPanel>();
    }

    public void OnSceneLateUpdate(float deltaTime)
    {
        
    }

    public void OnSceneUpdate(float deltaTime)
    {
        
    }
}
