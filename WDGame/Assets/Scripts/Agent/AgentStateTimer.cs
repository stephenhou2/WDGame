using GameEngine;

public enum AgentStateEndAction
{
    Remove,
    Add,
    Keep,
}

public class AgentStateTimer
{
    public BitType state;
    public float startTimestamp;
    public float endTimestamp;
    public float totalTime;
    public void Update(float now)
    {

    }
    
}
