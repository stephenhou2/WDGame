using GameEngine;
using System.Collections.Generic;

public abstract class Agent : IAgent
{
    protected int _entityId;
    protected AgentState _state;

    public abstract BitType GetAgentState();
    public abstract int GetAgentType();
    public abstract int GetEntityId();
    public abstract void OnAlive();
    public abstract void OnDead();
    public abstract void OnLateUpdate();
    public abstract void OnUpdate();
    public abstract void AddState(BitType state);
    public abstract void RemoveState(BitType state);
}