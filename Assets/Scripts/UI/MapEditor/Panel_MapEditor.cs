using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_MapEditor : UIPanel
{
    private GameObject _Node_Selection_Brush;
    private GameObject _Node_Slider_Brush;
    private GameObject _Node_Image_BrushSize;
    private GameObject _Node_Text_BrushSize;
    private GameObject _Node_InputField_Stage;
    private GameObject _Node_Button_LoadStage;
    private GameObject _Node_Button_SaveStage;

    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_BOTTOM_STATIC;
    }

    public override bool CheckArgs(object[] openArgs)
    {
        return true;
    }
    public override void OnOpen(object[] openArgs)
    {

    }

    /// <summary>
    /// 绑定UI
    /// </summary>
    protected override void BindUINodes()
    {
        // normal node
        BindNode(ref _Node_Selection_Brush, "_Selection_Brush");
        BindNode(ref _Node_Slider_Brush, "_Slider_Brush");
        BindNode(ref _Node_Image_BrushSize, "_Image_BrushSize");
        BindNode(ref _Node_Text_BrushSize, "_Text_BrushSize");

        // input field
        BindInputFieldNode(ref _Node_InputField_Stage, "_InputField_Stage", "0");

        // button
        BindButtonNode(ref _Node_Button_LoadStage, "_Button_LoadStage", OnLoadMapButtonClick);
        BindButtonNode(ref _Node_Button_SaveStage, "_Button_SaveStage", OnSaveMapButtonClick);

        InitializeBrushSelection();
        InitializeBrushSlider();
    }


    /// <summary>
    /// 初始化笔刷UI
    /// </summary>
    private void InitializeBrushSelection()
    {
        TMP_Dropdown brushSelect = _Node_Selection_Brush.GetComponent<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for(int i=0;i<(int)MapDefine.MapBrushType.Max;i++)
        {
            options.Add(new TMP_Dropdown.OptionData(MapDefine.BrushName[i]));
        }

        brushSelect.options = options;
        UIInterface.SetDropDownSelection(_Node_Selection_Brush, (int)MapDefine.MapBrushType.Obstacle);
    }

    /// <summary>
    /// 初始化笔刷slider
    /// </summary>
    private void InitializeBrushSlider()
    {
        RefreshSliderValue(GameMapEditor.Ins.setting.BrushWidth);

        UIInterface.AddSilderChangeAction(_Node_Slider_Brush, OnSliderValueChanged);
    }

    /// <summary>
    /// 刷新笔刷大小
    /// </summary>
    /// <param name="value"></param>
    private void RefreshSliderValue(int value)
    {
        UIInterface.SetTextString(_Node_Text_BrushSize, value.ToString());
        UIInterface.SetGraphicSize(_Node_Image_BrushSize, 10+value*2,10+value*2);
        UIInterface.SetSilderValue(_Node_Slider_Brush, value);
    }

    /// <summary>
    /// 笔刷大小改变
    /// </summary>
    /// <param name="value"></param>
    private void OnSliderValueChanged(float value)
    {
        int v = Mathf.FloorToInt(value);
        if(v != GameMapEditor.Ins.setting.BrushWidth)
        {
            GameMapEditor.Ins.setting.SetBrushWidth((int)value);
            RefreshSliderValue((int)value);
        }
    }

    /// <summary>
    /// 点击加载地图
    /// </summary>
    private void OnLoadMapButtonClick()
    {
        string mapId = UIInterface.GetInputFieldString(_Node_InputField_Stage);
        if (!string.IsNullOrEmpty(mapId))
        {
            bool ret = GameMapEditor.Ins.DataMgr.LoadMapData(mapId);
            if(!ret)
            {
                UIManager.Ins.OpenPanel<Panel_CreateNewMap>(new object[] { mapId });
            }
            else
            {
                GameMapEditor.Ins.LoadStageMap(mapId);
            }
        }
        else
        {
            Log.Error(ErrorLevel.Normal, "OnLoadMapButtonClick Error,empty mapId is invalid");
        }
    }

    /// <summary>
    /// 点击存储地图
    /// </summary>
    private void OnSaveMapButtonClick()
    {
        string mapId = UIInterface.GetInputFieldString(_Node_InputField_Stage);
        if (!string.IsNullOrEmpty(mapId))
        {
            GameMapEditor.Ins.DataMgr.SaveMapData(mapId);
        }
        else
        {
            Log.Error(ErrorLevel.Normal, "OnSaveMapButtonClick Error,empty mapId is invalid");
        }
    }

    public override void CustomClear()
    {
        _Node_Selection_Brush = null;
        _Node_Slider_Brush = null;
        _Node_Image_BrushSize = null;
        _Node_Text_BrushSize = null;
        _Node_InputField_Stage = null;
        _Node_Button_LoadStage = null;
        _Node_Button_SaveStage = null;
    }

    public override void OnClose()
    {

    }
}
