using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public static class PlotDataHelper
{
    private static DialogOptionData DecodeDialogOptionNode(XmlNode parent)
    {
        XmlElement element = parent as XmlElement;

        string title = element.GetAttribute("title");
        string content = element.GetAttribute("content");
        string img = element.GetAttribute("img");
        string sound = element.GetAttribute("sound");

        DialogOptionData option = new DialogOptionData()
        {
            Title = title,
            Content = content,
            Img = img,
            Sound = sound,
        };

        XmlNode followDialogNode = parent.SelectSingleNode("Dialog");
        if (followDialogNode != null)
        {
            DialogData followDialog = DecodeDialogNode(followDialogNode);
            option.FollowedDialog = followDialog;
        }

        return option;
    }

    private static DialogData DecodeDialogNode(XmlNode parent)
    {
        XmlElement element = parent as XmlElement;

        string title = element.GetAttribute("title");
        string content = element.GetAttribute("content");
        string img = element.GetAttribute("img");
        string sound = element.GetAttribute("sound");
        string video = element.GetAttribute("video");

        DialogData dialog = new DialogData()
        {
            Title = title,
            Content = content,
            Img = img,
            Sound = sound,
            Video = video
        };

        XmlNodeList optionNodes = parent.SelectNodes("Option");
        if (optionNodes != null)
        {
            dialog.Options = new DialogOptionData[optionNodes.Count];
            int index = 0;
            foreach (XmlNode node in optionNodes)
            {
                DialogOptionData opNode = DecodeDialogOptionNode(node);
                dialog.Options[index] = opNode;
                index++;
            }
        }

        XmlNode followDialogNode = parent.SelectSingleNode("Dialog");
        if (followDialogNode != null)
        {
            DialogData followDialog = DecodeDialogNode(followDialogNode);
            dialog.FollowedDialog = followDialog;
        }

        return dialog;
    }


    public static PlotData LoadPlotData(string plotId, bool optimize = false)
    {
        string plotFullPath = string.Format("{0}/{1}/.xml", PlotDef.PlotDataDir, plotId);

        if (!File.Exists(plotFullPath))
        {
            return null;
        }

        XmlDocument xml = new XmlDocument();
        xml.Load(plotFullPath);

        Queue<XmlNode> unhandleNodes = new Queue<XmlNode>();
        XmlNode root = xml.SelectSingleNode("root");
        if(root == null)
        {
            return null;
        }

        XmlNode firstDialogNode = root.SelectSingleNode("Dialog");
        if(firstDialogNode == null)
        {
            return null;
        }

        DialogData firstDialog = DecodeDialogNode(firstDialogNode);
        PlotData data = new PlotData(plotId, firstDialog);

        return data;
    }

    private static void EncodeOptionNode(XmlDocument xml,XmlNode parent,DialogOptionData option)
    {
        if (xml == null || parent == null || option == null)
        {
            return;
        }

        XmlElement optionNode = xml.CreateElement("Option");
        optionNode.SetAttribute("title", option.Title);
        optionNode.SetAttribute("content", option.Content);
        optionNode.SetAttribute("img", option.Img);
        optionNode.SetAttribute("sound", option.Sound);

        if (option.FollowedDialog != null)
        {
            EncodeDialogNode(xml, optionNode, option.FollowedDialog);
        }
    }

    private static void EncodeDialogNode(XmlDocument xml,XmlNode parent, DialogData dialog)
    {
        if(xml == null || parent == null || dialog == null)
        {
            return;
        }

        XmlElement dialogNode = xml.CreateElement("Dialog");
        dialogNode.SetAttribute("title", dialog.Title);
        dialogNode.SetAttribute("content", dialog.Content);
        dialogNode.SetAttribute("img", dialog.Img);
        dialogNode.SetAttribute("sound", dialog.Sound);
        dialogNode.SetAttribute("video", dialog.Video);

        if(dialog.Options != null && dialog.Options.Length >0)
        {
            for(int i=0;i<dialog.Options.Length;i++)
            {
                EncodeOptionNode(xml, dialogNode, dialog.Options[i]);
            }
        }

        if(dialog.FollowedDialog != null)
        {
            EncodeDialogNode(xml, dialogNode, dialog.FollowedDialog);
        }
    }

    public static void SavePlotData(string plotId, PlotData data, bool optimize = false)
    {
        if(data == null)
        {
            return;
        }

        string plotFullPath = string.Format("{0}/{1}/.xml", PlotDef.PlotDataDir, plotId);

        XmlDocument xml = new XmlDocument();
        XmlElement root = xml.CreateElement("root");
        xml.AppendChild(root);
        data.Reset();
        DialogData dialog = data.GetCurDialog();
        EncodeDialogNode(xml,root, dialog);

        xml.Save(plotFullPath);
    }
}
