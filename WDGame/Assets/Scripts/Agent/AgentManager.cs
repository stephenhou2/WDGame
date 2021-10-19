using GameEngine;
using System.Collections.Generic;


public class AgentManager : Singleton<AgentManager>
{
    private Dictionary<int, Hero> _heros = new Dictionary<int, Hero>();
    private Dictionary<int, Monster> _monsters = new Dictionary<int, Monster>();
    private Dictionary<int, NPC> _npcs = new Dictionary<int, NPC>();

    private GamePool<Hero> _heroPool = new GamePool<Hero>();
    private GamePool<Monster> _monsterPool = new GamePool<Monster>();
    private GamePool<NPC> _npcPool = new GamePool<NPC>();

    private int _curEntityId = 0;
    private int GenEntityId()
    {
        return _curEntityId++;
    }


    public Hero CreateHero(int heroId)
    {
        int entityId = GenEntityId();
        Hero hero = _heroPool.PopObj();
        if(hero == null)
        {
            hero = new Hero(entityId, heroId);
        }
        else
        { 
            hero.Initialize(entityId, heroId);
        }

        return hero ;
    }

    public void HeroToPool(Hero hero)
    {
        _heroPool.PushObj(hero);
    }

    public Monster CreateMonster(int monsterId)
    {
        return null;
    }

    public NPC CreateNpc(int npcId)
    {
        return null;
    }


}
