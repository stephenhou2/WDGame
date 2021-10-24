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
    
    public void Update(float deltaTime)
    {
        foreach(KeyValuePair<int,Hero> kv in (_heros))
        {
            Hero hero = kv.Value;
            if(hero != null)
            {
                hero.Update(deltaTime);
            }
        }
        foreach(KeyValuePair<int,Monster> kv in (_monsters))
        {
            Monster monster = kv.Value;
            if(monster != null)
            {
                monster.Update(deltaTime);
            }
        }
        foreach(KeyValuePair<int,NPC> kv in (_npcs))
        {
            NPC npc = kv.Value;
            if(npc != null)
            {
                npc.Update(deltaTime);
            }
        }
    }

    public void LateUpdate(float deltaTime)
    {
        foreach (KeyValuePair<int, Hero> kv in (_heros))
        {
            Hero hero = kv.Value;
            if (hero != null)
            {
                hero.LateUpdate(deltaTime);
            }
        }
        foreach (KeyValuePair<int, Monster> kv in (_monsters))
        {
            Monster monster = kv.Value;
            if (monster != null)
            {
                monster.LateUpdate(deltaTime);
            }
        }
        foreach (KeyValuePair<int, NPC> kv in (_npcs))
        {
            NPC npc = kv.Value;
            if (npc != null)
            {
                npc.LateUpdate(deltaTime);
            }
        }
    }

    public void Dispose()
    {

    }

    public Hero CreateHero(int heroId)
    {
        int entityId = GenEntityId();
        Hero hero = _heroPool.PopObj();
        if (hero == null)
        {
            hero = new Hero(entityId, heroId);
        }
        else
        {
            hero.Initialize(entityId, heroId);
        }

        if (!_heros.ContainsKey(entityId))
        {
            _heros.Add(entityId,hero);
        }

        return hero ;
    }

    public void HeroToPool(Hero hero)
    {
        int entityId = hero.GetEntityId();
        if (_heros.ContainsKey(entityId))
        {
            _heros.Remove(entityId);
        }
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
