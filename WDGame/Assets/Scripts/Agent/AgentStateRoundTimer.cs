/// <summary>
/// 使用回合的状态定时器
/// </summary>
public class AgentStateRoundTimer
{
    private int _startRound;
    private int _endRound;

    public AgentStateRoundTimer(int startRound, int endRound)
    {
        _startRound = startRound;
        _endRound = endRound;
    }

    public int GetLeftRound(int curRound)
    {
        return _endRound - curRound;
    }

    public bool RoundCheck(int curRound)
    {
        return GetLeftRound(curRound) <= 0;
    }

    public int GetStartRound()
    {
        return _startRound;
    }

    public int GetEndRound()
    {
        return _endRound;
    }

    public int GetTotalRound()
    {
        return _endRound - _startRound;
    }

    public void UpdateEndRound(int endRound)
    {
        _endRound = endRound;
    }
}

