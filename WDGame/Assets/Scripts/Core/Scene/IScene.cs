public interface IScene
{
    void OnSceneEnter();

    void OnSceneUpdate(float deltaTime);

    void OnSceneLateUpdate(float deltaTime);

    void OnSceneExit();
}
