using GameEngine;
public interface IAgent
{
    int GetEntityId();
    int GetAgentType();
    void OnAlive();
    void OnDead();
    void OnUpdate();
    void OnLateUpdate();
}