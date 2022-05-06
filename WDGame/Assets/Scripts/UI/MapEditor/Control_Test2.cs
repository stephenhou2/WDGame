using UnityEngine;
using GameEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Control_Test2 : UIControl
{
    private Button Button_Test;

    private void OnTestButtonClick()
    {
        Log.Logic("OnTestButtonClick 2222222222222");

        //UIEntity holder = GetHolder();
        //UIManager.Ins.RemoveControl(holder,this);
        //UIManager.Ins.ClosePanel<Panel_MapEditor>();
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

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string, object> openArgs)
    {
        Log.Logic("Control_Test OnOpen 22222222222");
    }

    public override void CustomClear()
    {
        Button_Test = null;
    }
}
