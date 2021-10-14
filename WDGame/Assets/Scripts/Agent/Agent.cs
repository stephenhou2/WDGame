using GameEngine;
using System.Collections.Generic;

public abstract class Agent : IAgent
{
    /// <summary>
    /// agent唯一id
    /// </summary>
    protected int _entityId;

    /// <summary>
    /// agent 状态
    /// </summary>
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