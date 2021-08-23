using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager mIns;
    public static UIManager Ins
    {
        get
        { 
            if (mIns == null)
            {
                mIns = new UIManager();
            }

            return mIns;
        }
    }

    private List<UIPanel> mAllPanels;


    private UIManager()
    {
        mAllPanels = new List<UIPanel>();
    }


    public void PushPanel(UIPanel panel)
    {
        if (panel == null)
        {
            return;
        }

        if(mAllPanels.Contains(panel))
        {
            return;
        }


    }

    public void RemovePanel(UIPanel panel)
    {

    }

}
