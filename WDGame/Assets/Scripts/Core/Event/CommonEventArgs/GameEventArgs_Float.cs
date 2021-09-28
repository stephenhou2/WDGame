using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventArgs_Float : GameEventArgs
{
    public float Value;
    public GameEventArgs_Float(float value)
    {
        this.Value = value;
    }
}
