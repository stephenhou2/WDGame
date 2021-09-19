using System.Collections;
using System.Collections.Generic;
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

        EmitterBus.AddListener(ModuleDef.MapEditor, "TestEvt", (GameEventArgs args) =>
        {
            TestEventArgs mArgs = args as TestEventArgs;
            if(mArgs != null)
            {
                Log.Logic(LogLevel.Hint,"this is a test event,testNum:{0},testStr:{1},testFloat:{2}",mArgs.testNum,mArgs.testStr,mArgs.testFloat);
            }
        });
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
