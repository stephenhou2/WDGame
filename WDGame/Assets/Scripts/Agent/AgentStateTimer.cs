using GameEngine;


public class AgentStateTimer
{
    private float _startTime;
    private float _endTime;

    public void InitAgentStateTimer(float startTime, float endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    public float GetLeftTime(float now)
    {
        return _endTime - now;
    }

    public bool TimerCheck(float now)
    {
        return GetLeftTime(now) <= 0;
    }

    public float GetStartTime()
    {
        return _startTime;
    }

    public float GetEndTime()
    {
        return _endTime;
    }

    public float GetTimerDuration()
    {
        return _endTime - _startTime;
    }

    public void UpdateEndStamp(float endTimeStamp)
    {
        _endTime = endTimeStamp;
    }
}
