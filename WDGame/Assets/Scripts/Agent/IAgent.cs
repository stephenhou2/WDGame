using GameEngine;
public interface IAgent
{
    int GetEntityId();
    int GetAgentType();
    void OnAlive();
    void OnDead();
    void OnUpdate(float deltaTime);
    void OnLateUpdate(float deltaTime);
}