using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventArgs_String : GameEventArgs
{
    public string Value;
    public GameEventArgs_String(string value)
    {
        this.Value = value;
    }
}
