using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOptionData
{
    /// <summary>
    /// 当前选项节点的标题
    /// </summary>
    public string Title;
    /// <summary>
    /// 当前选项节点的内容
    /// </summary>
    public string Content;
    /// <summary>
    /// 当前选项节点的配图
    /// </summary>
    public string Img;
    /// <summary>
    /// 当前选项节点的声音
    /// </summary>
    public string Sound;

    /// <summary>
    /// 当前选项的后续对话
    /// </summary>
    public DialogData FollowedDialog;
}
