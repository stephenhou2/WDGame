using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIControl : UIObject
{
    /// <summary>
    /// Control 加载时调用
    /// </summary>
    /// <param name="openArgs"></param>
    public abstract void OnAdd();


    /// <summary>
    /// Control移除前调用
    /// </summary>
    public abstract void OnRemove();

    protected void Remove()
    {
        OnRemove();
        ClearAll();
        GameObject.Destroy(mRoot);
    }
}
