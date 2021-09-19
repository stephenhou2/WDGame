using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameEventCallback(GameEventArgs args);
public class GameEvent
{
    List<GameEventCallback> mHandles = new List<GameEventCallback>();
    public void AddListener(GameEventCallback callback)
    {
        mHandles.Add(callback);
    }

    public void OnTrigger(GameEventArgs args)
    {
        foreach (var handle in mHandles)
        {
            if (handle != null)
            {
                handle(args);
            }
        }
    }

    public void Dispose()
    {
        mHandles.Clear();
    }
}