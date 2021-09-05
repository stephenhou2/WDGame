﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Test2 : UIControl
{
    private GameObject Button_Test;

    private void OnTestButtonClick()
    {
        Log.Logic("OnTestButtonClick 2222222222222");

        UIEntity holder = GetHolder();
        UIManager.Ins.RemoveControl(holder,this);
    }

    public override int GetRecycleStrategy()
    {
        return UIDefine.UI_Recycle_UIEntity | UIDefine.UI_Recycle_UIGameObject;
    }

    protected override void BindUINodes()
    {
        BindButtonNode(ref Button_Test, "Button_Test", OnTestButtonClick);
    }

    protected override void OnClose()
    {
        Log.Logic("Control_Test OnClose 2222222222");
    }

    protected override void OnOpen()
    {
        Log.Logic("Control_Test OnOpen 22222222222");
    }

    public override void CustomClear()
    {
        Button_Test = null;
    }
}
