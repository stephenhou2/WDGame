using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace GameEngine
{
    public abstract partial class UIEntity
    {
        protected int BindControl<T>(ref T ctl, string node, string uiPath) where T : UIControl, new()
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
                Log.Error(ErrorLevel.Critical, "BindNode failed,does not has target node,node={0},UIEntity is:{1}", node, this.GetType());
                return -2;
            }
            go = targetNode.gameObject; // 绑定成功
            return 0;
        }

        protected int BindImageNode(ref Image img,string node,Sprite defaultSprite = null)
        {
            GameObject go = null;
            int ret = BindNode(ref go, node);
            if (ret < 0) // 节点绑定失败
                return ret;

            img = go.GetComponent<Image>();
            if (img == null) //没有Image组件
            {
                Log.Error(ErrorLevel.Critical, "BindImageNode failed,Image component is Required! node={0}", node);
                return -3;
            }

            if (defaultSprite != null)
                img.sprite = defaultSprite;

            return 0;
        }


        protected int BindButtonNode(ref Button btn, string node, UnityAction call = null)
        {
            GameObject go = null;
            int ret = BindNode(ref go, node);
            if (ret < 0) // 节点绑定失败
                return ret;

            btn = go.GetComponent<Button>();
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

        protected int BindTextNode(ref TMP_Text tmp_text, string node, string defaultText = null)
        {
            GameObject go = null;
            int ret = BindNode(ref go, node);
            if (ret < 0) // 节点绑定失败
                return ret;

            tmp_text = go.GetComponent<TMP_Text>();
            if (tmp_text == null) //没有TMP_Text组件
            {
                Log.Error(ErrorLevel.Critical, "BindTextNode failed,TMP_Text component is Required! node={0}", node);
                return -3;
            }

            if (defaultText != null)
                tmp_text.text = defaultText;

            return 0;
        }

        protected int BindInputFieldNode(ref TMP_InputField tmp_field, string node, string defaultText = null, UnityAction<string> valueChangeCb = null)
        {
            GameObject go = null;
            int ret = BindNode(ref go, node);
            if (ret < 0) // 节点绑定失败
                return ret;

            tmp_field = go.GetComponent<TMP_InputField>();
            if (tmp_field == null) //没有TMP_InputField组件
            {
                Log.Error(ErrorLevel.Critical, "BindInputFieldNode failed,TMP_InputField component is Required! node={0}", node);
                return -3;
            }

            if(valueChangeCb != null)
            {
                tmp_field.onValueChanged.AddListener(valueChangeCb);
            }

            if (defaultText != null)
                tmp_field.text = defaultText;

            return 0;
        }
    }
}