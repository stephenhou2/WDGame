using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : UIObject
{
    public abstract string GetPanelLayerPath();
    public abstract string GetPanelResPath();

    /// <summary>
    /// panel 打开时调用
    /// </summary>
    public abstract void OnOpen();

    /// <summary>
    /// panel关闭前调用
    /// </summary>
    public abstract void OnClose();
}
