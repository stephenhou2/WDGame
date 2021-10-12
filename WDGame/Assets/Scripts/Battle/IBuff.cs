using GameEngine;

public interface IBuff
{
    void OnRoundEnter();
    void OnRoundExit();
    void OnBuffFirstAdd();
    void OnBuffRemove();
    void OnUpdate();
    void Dispose();
    BitType GetBuffType();
}
