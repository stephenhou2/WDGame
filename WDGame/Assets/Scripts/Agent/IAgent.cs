using GameEngine;
public interface IAgent
{
    int GetEntityId();

    int GetAgentType();

    GameEngine.BitType GetAgentState();

    void OnAlive();

    void OnDead();

    void OnUpdate();

    void OnLateUpdate();

    void AddState(BitType state);
    void RemoveState(BitType state);
}