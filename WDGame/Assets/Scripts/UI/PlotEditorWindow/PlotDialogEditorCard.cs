using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using GameEngine;

public class PlotDialogEditorCard : PlotEditorCard
{
    private string TitleInput = "TOP/TITLE_INPUT";
    private string UnfoldBtn = "TOP/BUTTON_FOLD";
    private string ImgSelect = "TOP/IMAGE_INPUT_SHOW";
    private string SoundSelect = "TOP/SOUND_ADD";
    private string SoundName = "TOP/SOUND_ADD/SOUND_NAME";
    private string SoundPlay = "TOP/SOUND_PLAY";
    private string VideoSelect = "TOP/VIDEO_ADD";
    private string VideoName = "TOP/VIDEO_ADD/VIDEO_NAME";
    private string VideoPlay = "TOP/VIDEO_PLAY";
    private string ContentInput = "TOP/CONTENT_INPUT";
    private string ConnectorRight = "CONNECT_NODE_RIGHT";
    private string ConnectorLeft = "CONNECT_NODE_LEFT";
    private string AddDialogBtn = "ADD_DIALOG";
    private string AddOptionBtn = "ADD_OPTION";

    private TMP_InputField _titleInput;
    private Button _unfoldBtn;
    private Button _imgSelect;
    private Button _soundSelect;
    private TMP_Text _soundName;
    private Button _soundPlay;
    private Button _videoSelect;
    private TMP_Text _videoName;
    private Button _videoPlay;
    private GameObject _connectorLeft;
    private GameObject _connectorRight;
    private Button _addDialogBtn;
    private Button _addOptionBtn;

    protected override void BindUINodes()
    {
        BindInputFieldNode(ref _titleInput, TitleInput);
        BindButtonNode(ref _unfoldBtn, UnfoldBtn,Unfold);
        BindButtonNode(ref _imgSelect, ImgSelect,SelectImg);
        BindButtonNode(ref _soundSelect, SoundSelect,SelectSound);
        BindTextNode(ref _soundName, SoundName);
        BindButtonNode(ref _soundPlay, SoundPlay,PlaySound);
        BindButtonNode(ref _videoSelect, VideoSelect,SelectVideo);
        BindTextNode(ref _videoName, VideoName);
        BindButtonNode(ref _videoPlay, VideoPlay,PlayVideo);
        BindNode(ref _connectorLeft, ConnectorLeft);
        BindNode(ref _connectorRight, ConnectorRight);
        BindButtonNode(ref _addDialogBtn, AddDialogBtn,AddDialog);
        BindButtonNode(ref _addOptionBtn, AddOptionBtn,AddOption);
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string, object> openArgs)
    {
       
    }

    protected override void OnClose()
    {
        
    }

    public override void CustomClear()
    {
        
    }

    public override void CardUpdate(float deltaTime)
    {
        
    }

    public void SetPosition(Vector3 postion)
    {
        var go = GetRootObj();
        GetRootObj().transform.localPosition = postion;
    }


    private void Unfold()
    {

    }

    private void SelectImg()
    {
        UIManager.Ins.OpenPanel<PlotEditSelectImageWindow>("Prefab/_IMG_SELECTOR");
    }

    private void SelectSound()
    {

    }

    private void PlaySound()
    {

    }

    private void SelectVideo()
    {

    }

    private void PlayVideo()
    {

    }

    private void AddDialog()
    {

    }

    private void AddOption()
    {

    }
}
