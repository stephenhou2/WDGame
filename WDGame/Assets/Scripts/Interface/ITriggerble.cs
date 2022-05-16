using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerble
{
    bool CheckTrigger();
    void OnTrigger(Agent[] srcs, Agent[] targets);
}
