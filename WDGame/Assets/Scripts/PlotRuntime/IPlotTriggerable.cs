using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlotTriggerable
{
    bool PlotTriggered(string triggerType,string compare, string triggerValue);
}
