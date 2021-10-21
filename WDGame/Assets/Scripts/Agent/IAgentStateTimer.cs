using UnityEngine;
using System;

public interface IAgentStateTimer
{
    bool TimerCheck();
    void Set(float v);
    float Get();
}

public class Test
{
    void foo2(float v)
    {

    }

    void foo3()
    {
        int a = 1;
        foo2(a);
    }

    void foo(Vector3 v)
    {
        
    } 
    void foo4(Vector3Int v)
    {
        
    }

    void foo1()
    {
        //foo(Vector3Int.zero);
    }
}


