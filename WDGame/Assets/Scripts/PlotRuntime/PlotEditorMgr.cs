using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotEditorMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlotEditDataCenter.Ins.InitializePlotEditDataCenter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
