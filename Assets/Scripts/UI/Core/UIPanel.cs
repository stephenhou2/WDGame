using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : UIObject
{
    public abstract string GetPanelLayerPath();
    public abstract string GetPanelResPath();

    public string panelType
    {
        get { return GetType().ToString(); }
    }

    /// <summary>
    /// panel 打开时调用
    /// </summary>
    /// <param name="openArgs"></param>
    public abstract void OnOpen(object[] openArgs);

    /// <summary>
    /// panel关闭前调用
    /// </summary>
    public abstract void OnClose();

    protected void Close(string panelType)
    {
        UIManager.Ins.ClosePanel(panelType);
    }


}
