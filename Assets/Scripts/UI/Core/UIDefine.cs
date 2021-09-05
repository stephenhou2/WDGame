using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIDefine 
{
    public const int Panel_Load_Per_Frame = 1; // 每帧最多加载的面板数量
    public const int Control_Load_Per_Frame = 5; // 每帧最多加载的Control数量


    public static readonly int UI_Recycle_DontRecycle = 0;
    public static readonly int UI_Recycle_UIEntity = 1;  // 复用组件
    public static readonly int UI_Recycle_UIGameObject = 2; // 复用gameobject
}
