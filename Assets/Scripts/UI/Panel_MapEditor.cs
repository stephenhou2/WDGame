using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_MapEditor : UIPanel
{
    private GameObject _Node_InputField_MapWidth;
    private GameObject _Node_InputField_MapHeight;
    private GameObject _Node_InputField_CellSize;
    private GameObject _Node_Selection_Direction;
    private GameObject _Node_Button_DrawGrids;
    private GameObject _Node_Selection_Brush;
    private GameObject _Node_Slider_Brush;
    private GameObject _Node_Image_BrushSize;
    private GameObject _Node_Text_BrushSize;
    private GameObject _Node_InputField_Stage;
    private GameObject _Node_Button_LoadStage;
    private GameObject _Node_Button_SaveStage;

    public Panel_MapEditor(GameObject panelGo)
    {
        mPanelRoot = panelGo;
        if (mPanelRoot != null)
        {
            BindUINodes();
        }
    }

    public override void OnClose()
    {
        
    }

    public override void OnOpen(PanelOpenArgs openArgs)
    {
        
    }

    private void InitializeCellDirSelection()
    {
        TMP_Dropdown cellDirSelect = _Node_Selection_Direction.GetComponent<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        var option_verticle = new TMP_Dropdown.OptionData("Verticle");
        var option_horizontal = new TMP_Dropdown.OptionData("Horizontal");
        options.Add(option_verticle);
        options.Add(option_horizontal);

        cellDirSelect.options = options;

        UIInterface.SetDropDownSelection(_Node_Selection_Direction, (int)GameMapEditor.Ins.setting.CellDirection);
    }

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

    private void InitializeBrushSlider()
    {
        RefreshSliderValue(GameMapEditor.Ins.setting.BrushWidth);

        UIInterface.AddSilderChangeAction(_Node_Slider_Brush, OnSliderValueChanged);
    }

    private void RefreshSliderValue(int value)
    {
        UIInterface.SetTextString(_Node_Text_BrushSize, value.ToString());
        UIInterface.SetGraphicSize(_Node_Image_BrushSize, 10+value*2,10+value*2);
        UIInterface.SetSilderValue(_Node_Slider_Brush, value);
    }

    private void OnSliderValueChanged(float value)
    {
        int v = Mathf.FloorToInt(value);
        if(v != GameMapEditor.Ins.setting.BrushWidth)
        {
            GameMapEditor.Ins.setting.SetBrushWidth((int)value);
            RefreshSliderValue((int)value);
        }
    }

    private void InitializeStageLoad()
    {
        UIInterface.SetInputFieldString(_Node_InputField_Stage, "-1");
        UIInterface.AddButtonAction(_Node_Button_LoadStage, OnLoadStageButtonClick);
        UIInterface.AddButtonAction(_Node_Button_SaveStage, OnLoadStageButtonClick);
    }

    private void OnLoadStageButtonClick()
    {
        string stageStr = UIInterface.GetInputFieldString(_Node_InputField_Stage);
        int stageId = int.Parse(stageStr);
        GameMapEditor.Ins.DataMgr.LoadMapData(stageId);
        OnDrawGridsButtonClick();
    }

    private void OnSaveStageButtonClick()
    {
        string stageStr = UIInterface.GetInputFieldString(_Node_InputField_Stage);
        int stageId = int.Parse(stageStr);
        GameMapEditor.Ins.DataMgr.LoadMapData(stageId);
        OnDrawGridsButtonClick();
    }

    private void InitializeUI()
    {
        InitializeCellDirSelection();
        InitializeBrushSelection();
        InitializeBrushSlider();
        InitializeStageLoad();

        UIInterface.AddButtonAction(_Node_Button_DrawGrids, OnDrawGridsButtonClick);
        UIInterface.SetInputFieldString(_Node_InputField_MapWidth, GameMapEditor.Ins.setting.MapWidth.ToString());
        UIInterface.SetInputFieldString(_Node_InputField_MapHeight, GameMapEditor.Ins.setting.MapHeight.ToString());
        UIInterface.SetInputFieldString(_Node_InputField_CellSize, GameMapEditor.Ins.setting.MapCellSize.ToString());
    }

    private void OnDrawGridsButtonClick()
    {
        string mapWidthStr = UIInterface.GetInputFieldString(_Node_InputField_MapWidth);
        string mapHeightStr = UIInterface.GetInputFieldString(_Node_InputField_MapHeight);
        string cellSizeStr = UIInterface.GetInputFieldString(_Node_InputField_CellSize);
        int mapWidth = int.Parse(mapWidthStr);
        int mapHeight = int.Parse(mapHeightStr);
        float cellSize = float.Parse(cellSizeStr);
        int selection = UIInterface.GetDropDownSelection(_Node_Selection_Direction);


        GameMapEditor.Ins.setting.SetMapWidth(mapWidth);
        GameMapEditor.Ins.setting.SetMapHeight(mapHeight);
        GameMapEditor.Ins.setting.SetMapCellSize(cellSize);
        GameMapEditor.Ins.setting.SetMapCellDirection((MapCellDirection)selection);
        GameMapEditor.Ins.DoDraw(MapDefine.MapDrawer_Ground);
    }

    protected override void BindUINodes()
    {
        _Node_InputField_MapWidth = UIInterface.FindChildNode(mPanelRoot.transform,"_InputField_MapWidth").gameObject;
        _Node_InputField_MapHeight = UIInterface.FindChildNode(mPanelRoot.transform, "_InputField_MapHeight").gameObject;
        _Node_InputField_CellSize = UIInterface.FindChildNode(mPanelRoot.transform, "_InputField_CellSize").gameObject;
        _Node_Selection_Direction = UIInterface.FindChildNode(mPanelRoot.transform, "_Selection_Direction").gameObject;
        _Node_Button_DrawGrids = UIInterface.FindChildNode(mPanelRoot.transform, "_Button_DrawGrids").gameObject;
        _Node_Selection_Brush = UIInterface.FindChildNode(mPanelRoot.transform, "_Selection_Brush").gameObject;
        _Node_Slider_Brush = UIInterface.FindChildNode(mPanelRoot.transform, "_Slider_Brush").gameObject;
        _Node_Image_BrushSize = UIInterface.FindChildNode(mPanelRoot.transform, "_Image_BrushSize").gameObject;
        _Node_Text_BrushSize = UIInterface.FindChildNode(mPanelRoot.transform, "_Text_BrushSize").gameObject;
        _Node_InputField_Stage = UIInterface.FindChildNode(mPanelRoot.transform, "_InputField_Stage").gameObject;
        _Node_Button_LoadStage = UIInterface.FindChildNode(mPanelRoot.transform, "_Button_LoadStage").gameObject;
        _Node_Button_SaveStage = UIInterface.FindChildNode(mPanelRoot.transform, "_Button_SaveStage").gameObject;

        InitializeUI();
    }




}
