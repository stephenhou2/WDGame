using GameEngine;
using UnityEngine;

public class Control_Test : UIControl
{
    private GameObject Button_Test;

    private void OnTestButtonClick()
    {
        Log.Logic("OnTestButtonClick");

        //UIManager.Ins.RemoveControl(mHolder,this);

        UIManager.Ins.AddControl<Control_Test2>(this, "UI/MapEditor/Control_Test2", mUIRoot);
    }
    
    protected override void BindUINodes()
    {
        BindButtonNode(ref Button_Test, "Button_Test", OnTestButtonClick);
    }

    protected override void OnClose()
    {
        Log.Logic("Control_Test OnClose");
    }

    protected override void OnOpen()
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
