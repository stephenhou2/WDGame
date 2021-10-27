using GameEngine;

public interface IBuff
{
    void OnEnterBattle(Agent agent);
    void OnExitBattle(Agent agent);
    void OnRoundEnter();
    void OnRoundExit();
    void OnBuffFirstAdd();
    void OnBuffRemove();
    void OnUpdate();
    void Dispose();
    BitType GetBuffType();
    int GetBuffNum();
    void Merge(IBuff buff);
}
