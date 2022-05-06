using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData
{
    /// <summary>
    /// 当前对话节点的标题id
    /// </summary>
    public string Title;
    /// <summary>
    /// 当前对话节点的内容
    /// </summary>
    public string Content;
    /// <summary>
    /// 当前对话节点的配图
    /// </summary>
    public string Img;
    /// <summary>
    /// 当前对话节点的声音
    /// </summary>
    public string Sound;
    /// <summary>
    ///  当前对话节点的视频
    /// </summary>
    public string Video;
    /// <summary>
    /// 当前对话节点的选项数组
    /// </summary>
    public DialogOptionData[] Options;
    /// <summary>
    /// 当前对话节点的后续对话
    /// </summary>
    public DialogData FollowedDialog;
}
