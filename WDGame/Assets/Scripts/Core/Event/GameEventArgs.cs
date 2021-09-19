using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventArgs
{

}

public class TestEventArgs:GameEventArgs
{
    public int testNum;
    public string testStr;
    public float testFloat;
}
