using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventArgs_Int : GameEventArgs
{
    public int Value;
    public GameEventArgs_Int(int value)
    {
        this.Value = value;
    }
}
