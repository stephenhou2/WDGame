using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public abstract partial class UIEntity
{
    protected int BindControl<T>(ref T ctl, string node, string uiPath) where T : UIControl,new()
    {
        if (mUIRoot == null) // 没有根节点
        {
            Log.Error(ErrorLevel.Critical, "BindControl failed,ui root node is null!");
            return -1;
        }

        Transform targetNode = UIInterface.FindChildNode(mUIRoot.transform, node);
        if (targetNode == null)// 找不到要绑定的节点
        {
            Log.Error(ErrorLevel.Critical, "BindControl failed,does not has target node,node={0}", node);
            return -2;
        }

        ctl = UIManager.Ins.BindControl<T>(this, targetNode.gameObject, uiPath);
        return 0;
    }

    protected int BindNode(ref GameObject go, string node)
    {
        if (mUIRoot == null) // 没有根节点
        {
            Log.Error(ErrorLevel.Critical, "BindNode failed,ui root node is null!");
            return -1;
        }

        Transform targetNode = UIInterface.FindChildNode(mUIRoot.transform, node);
        if (targetNode == null)// 找不到要绑定的节点
        {
            Log.Error(ErrorLevel.Critical, "BindNode failed,does not has target node,node={0},UIEntity is:{1}", node,this.GetType());
            return -2;
        }
        go = targetNode.gameObject; // 绑定成功
        return 0;
    }

    protected int BindButtonNode(ref GameObject go, string node, UnityAction call = null)
    {
        int ret = BindNode(ref go, node);
        if (ret < 0) // 节点绑定失败
            return ret;

        Button btn = go.GetComponent<Button>();
        if (btn == null) //没有Button组件
        {
            Log.Error(ErrorLevel.Critical, "BindButtonNode failed,Button component is Required! node={0}", node);
            return -3;
        }

        if (call != null) //没有绑定回调（可以不绑）
        {
            btn.onClick.AddListener(call);
            mUIEvents.Add(btn.onClick);
        }

        return 0;
    }

    protected int BindInputFieldNode(ref GameObject go, string node, string defaultText = null)
    {
        int ret = BindNode(ref go, node);
        if (ret < 0) // 节点绑定失败
            return ret;

        TMP_InputField field = go.GetComponent<TMP_InputField>();
        if (field == null) //没有TMP_InputField组件
        {
            Log.Error(ErrorLevel.Critical, "BindInputFieldNode failed,TMP_InputField component is Required! node={0}", node);
            return -3;
        }

        if (defaultText != null)
            field.text = defaultText;

        return 0;
    }
}
