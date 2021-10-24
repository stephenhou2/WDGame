/// <summary>
/// 使用时间的状态定时器
/// </summary>
public class AgentStateTimer
{
    private float _startTime;
    private float _endTime;

    public static AgentStateTimer Create(float duration)
    {
        return new AgentStateTimer(TimeMgr.Now, TimeMgr.Now + duration);
    }

    public AgentStateTimer(float startTime, float endTime)
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
