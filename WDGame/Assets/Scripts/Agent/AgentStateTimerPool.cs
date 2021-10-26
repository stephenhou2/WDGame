using System.Collections.Generic;
using GameEngine;

public class AgentStateTimerPool
{
    private Dictionary<BitType, AgentStateTimer> _stateTimers = new Dictionary<BitType, AgentStateTimer>();
    private List<BitType> _toRemoveStateTimers = new List<BitType>();

    public void SetStateTimer(BitType state, AgentStateTimer timer)
    {
        AddStateTimer(state, timer);
    }

    private void UpdateStateTimeTimers(AgentStateData agentState)
    {
        _toRemoveStateTimers.Clear();

        foreach (KeyValuePair<BitType, AgentStateTimer> kv in _stateTimers)
        {
            BitType state = kv.Key;
            AgentStateTimer timer = kv.Value;

            if (timer != null && timer.TimerCheck(TimeMgr.Now))
            {
                DefaultActionOnStateEnd(agentState,state);
                _toRemoveStateTimers.Add(kv.Key);
            }
        }

        for (int i = 0; i < _toRemoveStateTimers.Count; i++)
        {
            BitType state = _toRemoveStateTimers[i];
            RemoveStateTimer(state);
        }
    }

    public void Update(float deltaTime,AgentStateData agentState)
    {
        UpdateStateTimeTimers(agentState);
    }


    public AgentStateTimer GetStateTimer(BitType state)
    {
        if (_stateTimers.TryGetValue(state, out AgentStateTimer timer))
        {
            return timer;
        }

        return null;
    }

    public void AddStateTimer(BitType state, AgentStateTimer timer)
    {
        var _timer = GetStateTimer(state);
        if (_timer == null)
        {
            _stateTimers.Add(state, timer);
        }
        else if (_timer.GetEndTime() < timer.GetEndTime())
        {
            _stateTimers[state] = timer;
        }
    }

    public void RemoveStateTimer(BitType state)
    {
        if (_stateTimers.ContainsKey(state))
        {
            _stateTimers.Remove(state);
        }
    }

    private void DefaultActionOnStateEnd(AgentStateData agentState,BitType state)
    {
        if (agentState.DefaultHasState(state))
        {
            agentState.AddState(state);
        }
        else
        {
            agentState.RemoveState(state);
        }
    }
}
