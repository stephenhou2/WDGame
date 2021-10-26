using GameEngine;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateData
{
    private BitType _state;
    private BitType _defaultState;

    public AgentStateData(BitType defaultState)
    {
        _state = defaultState;
        _defaultState = defaultState.Clone(false);
        Log.Logic(LogLevel.Hint, "InitializeAgentDefaultState State:{0}", defaultState.ToString());
    }


    public BitType GetAgentState()
    {
        return _state;
    }

    public void AddState(BitType state)
    {
        if(_state != null)
            _state.BindBitType(state);

        Log.Logic(LogLevel.Hint, "Add State:{0}, cur State:{1}", state.ToString(),_state.ToString());
    }

    public void RemoveState(BitType state)
    {
        if(_state != null)
            _state.RemoveBitType(state);

        Log.Logic(LogLevel.Hint, "RemoveState State:{0}, cur State:{1}", state.ToString(), _state.ToString());
    }

    public bool DefaultHasState(BitType state)
    {
        return _defaultState.HasType(state);
    }
}
