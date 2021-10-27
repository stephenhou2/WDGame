
using GameEngine;

public interface IBattle
{
    void OnEnterBattle();
    void OnExitBattle();
    void CasterSkill(ISkill skill, Agent[] targets);
    void AddBuff(IBuff buff);
    void RemoveBuff(BitType buffType);
}
