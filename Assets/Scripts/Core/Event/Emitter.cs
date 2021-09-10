using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEmitterOption
{
        void OnFirstListenerAdd();              // 第一次注册监听回调
        void OnFirstListenerDidAdd();        // 第一次注册监听完成回调
        void OnListenerDidAdd();                // 当有事件被监听成功
        void OnLastListenerRemove();        // 当最后一个事件被移除
}


public class EmitterBus
{
    public Dictionary<BitType, Emitter> mAllEmitters = new Dictionary<BitType, Emitter>();
}

public class EmitterOption : IEmitterOption
{
    public void OnFirstListenerAdd()
    {
        throw new System.NotImplementedException();
    }

    public void OnFirstListenerDidAdd()
    {
        throw new System.NotImplementedException();
    }

    public void OnLastListenerRemove()
    {
        throw new System.NotImplementedException();
    }

    public void OnListenerDidAdd()
    {
        throw new System.NotImplementedException();
    }
}

public class Emitter
{
    private EmitterOption mOption;

    private BitType mType;

    private Dictionary<string, GameEvent> mListeners;

}

public class GameEvent
{
    public void AddListener()
    {

    }
}
