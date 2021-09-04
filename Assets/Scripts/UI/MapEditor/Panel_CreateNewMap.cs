using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Panel_CreateNewMap : UIPanel
{
    private GameObject _Node_InputField_MapWidth;
    private GameObject _Node_InputField_MapHeight;
    private GameObject _Node_InputField_CellSize;
    private GameObject _Node_Selection_Direction;
    private GameObject _Node_Button_CreateMap;
    private GameObject _Node_Button_Back;

    private string mMapId;

    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_NORMAL_STATIC;
    }

    public override string GetPanelResPath()
    {
        return "UI/MapEditor/Panel_CreateNewMap";
    }

    protected override void OnOpen()
    {
        
    }

    public void Initialize(string mapId)
    {
        mMapId = mapId;
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

        UIInterface.SetDropDownSelection(_Node_Selection_Direction, (int)GameMapEditor.Ins.MapConfig.CellDirection);
    }

    private void OnCreateNewMapButtonClick()
    {
        string mapWidthStr = UIInterface.GetInputFieldString(_Node_InputField_MapWidth);
        string mapHeightStr = UIInterface.GetInputFieldString(_Node_InputField_MapHeight);
        string cellSizeStr = UIInterface.GetInputFieldString(_Node_InputField_CellSize);
        int mapWidth = int.Parse(mapWidthStr);
        int mapHeight = int.Parse(mapHeightStr);
        float cellSize = float.Parse(cellSizeStr);
        int selection = UIInterface.GetDropDownSelection(_Node_Selection_Direction);

        GameMapEditor.Ins.CreateNewMap(mMapId, mapWidth, mapHeight, selection, cellSize);

        UIManager.Ins.ClosePanel<Panel_CreateNewMap>();
    }

    private void InitializeUI()
    {
        InitializeCellDirSelection();
    }

    protected override void BindUINodes()
    {
        // normal node
        BindNode(ref _Node_Selection_Direction, "_Selection_Direction");

        // input field
        BindInputFieldNode(ref _Node_InputField_MapWidth, "_InputField_MapWidth", GameMapEditor.Ins.MapConfig.MapWidth.ToString());
        BindInputFieldNode(ref _Node_InputField_MapHeight, "_InputField_MapHeight", GameMapEditor.Ins.MapConfig.MapHeight.ToString());
        BindInputFieldNode(ref _Node_InputField_CellSize, "_InputField_CellSize", GameMapEditor.Ins.MapConfig.MapCellSize.ToString());

        // button
        BindButtonNode(ref _Node_Button_CreateMap, "_Button_CreateMap", OnCreateNewMapButtonClick);
        BindButtonNode(ref _Node_Button_Back, "_Button_Back",()=>
        {
            UIManager.Ins.ClosePanel<Panel_CreateNewMap>();
        });

        InitializeUI();
    }

    public override void CustomClear()
    {
        mMapId = string.Empty;

        _Node_InputField_MapWidth = null;
        _Node_InputField_MapHeight = null;
        _Node_InputField_CellSize = null;
        _Node_Selection_Direction = null;
        _Node_Button_CreateMap = null;
        _Node_Button_Back = null;
    }

    protected override void OnClose()
    {

    }
}
