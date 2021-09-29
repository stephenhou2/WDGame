public interface IScene
{
    string GetSceneName();

    void OnSceneEnter();

    void OnSceneUpdate(float deltaTime);

    void OnSceneLateUpdate(float deltaTime);

    void OnSceneExit();
}
