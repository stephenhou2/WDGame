using GameEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Test : UIControl
{
    private Button Button_Test;

    private void OnTestButtonClick()
    {
        Log.Logic("OnTestButtonClick");

        //UIManager.Ins.RemoveControl(mHolder,this);

        UIManager.Ins.AddControl<Control_Test2>(this, "UIPrefab/MapEditor/Control_Test2", mUIRoot);
    }
    
    protected override void BindUINodes()
    {
        BindButtonNode(ref Button_Test, "Button_Test", OnTestButtonClick);
    }

    protected override void OnClose()
    {
        Log.Logic("Control_Test OnClose");
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string,object> openArgs)
    {
        Log.Logic("Control_Test OnOpen");
    }

    public override void CustomClear()
    {
        Button_Test = null;
    }

    public override int GetRecycleStrategy()
    {
        return UIDefine.UI_Recycle_UIEntity | UIDefine.UI_Recycle_UIGameObject;
    }
}
