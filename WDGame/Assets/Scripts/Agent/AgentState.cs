using GameEngine;

public class AgentState
{
    public BitType _state;

    public void SetAgentState(BitType state)
    {
        _state = state;
    }

    public BitType GetAgentState()
    {
        return _state;
    }

    public void AddState(BitType state)
    {
        if(_state != null)
            _state.BindBitType(state);
    }

    public void RemoveState(BitType state)
    {
        if(_state != null)
            _state.RemoveBitType(state);
    }
}
