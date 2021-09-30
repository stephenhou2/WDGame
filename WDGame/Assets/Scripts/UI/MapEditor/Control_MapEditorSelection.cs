using System.Collections.Generic;
using UnityEngine;
using GameEngine;

public class Control_MapEditorSelection : UIControl
{
    private GameObject _Node_Button_Obstacle;
    private GameObject _Node_Button_Select;

    private GameObject _Node_InputField_Stage;
    private GameObject _Node_Button_LoadStage;
    private GameObject _Node_Button_SaveStage;

    private Color DeselectColor = Color.white;
    private Color SelectColor = Color.green;

    private List<GameObject> mAllSelections;
    private int mCurSelection = 0;

    protected override void OnOpen()
    {
        mCurSelection = 0;
        mAllSelections = new List<GameObject>();
        mAllSelections.Add(_Node_Button_Select);
        mAllSelections.Add(_Node_Button_Obstacle);
    }

    private void Select(int indexId)
    {
        mCurSelection = indexId;
        for(int i =0;i<mAllSelections.Count;i++)
        {
            UIInterface.SetGraphicColor(mAllSelections[i], mCurSelection == i ? SelectColor : DeselectColor);
        }
    }

    /// <summary>
    /// 点击加载地图
    /// </summary>
    private void OnLoadMapButtonClick()
    {
        string mapId = UIInterface.GetInputFieldString(_Node_InputField_Stage);
        if (string.IsNullOrEmpty(mapId))
        {
            Log.Error(ErrorLevel.Normal, "OnLoadMapButtonClick Error,empty mapId is invalid");
            return;
        }

        bool ret = GameMapEditor.Ins.DataMgr.HasMapData(mapId);
        if (!ret) // 没有地图数据，弹出创建地图面板
        {
            UIManager.Ins.OpenPanel<Panel_CreateNewMap>("UI/MapEditor/Panel_CreateNewMap", (UIEntity obj) =>
            {
                Panel_CreateNewMap panel = obj as Panel_CreateNewMap;
                panel.Initialize(mapId);
            });
            return;
        }

        GameMapEditor.Ins.LoadStageMap(mapId);
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

    private void OnSelectButtonClick()
    {
        Select(0);
        Log.Logic(LogLevel.Normal, "click select");
    }

    private void OnObstacleButtonClick()
    {
        Select(1);
        Log.Logic(LogLevel.Normal, "click obstacle");
    }

    protected override void BindUINodes()
    {
        // input field
        BindInputFieldNode(ref _Node_InputField_Stage, "_InputField_Stage", "0");

        // button
        BindButtonNode(ref _Node_Button_Select, "_Button_Select", OnSelectButtonClick);
        BindButtonNode(ref _Node_Button_Obstacle, "_Button_Obstacle", OnObstacleButtonClick);

        BindButtonNode(ref _Node_Button_LoadStage, "_Button_LoadStage", OnLoadMapButtonClick);
        BindButtonNode(ref _Node_Button_SaveStage, "_Button_SaveStage", OnSaveMapButtonClick);
    }


    public override void CustomClear()
    {
        _Node_Button_Obstacle = null;
        _Node_Button_Select = null;

        _Node_InputField_Stage = null;
        _Node_Button_LoadStage = null;
        _Node_Button_SaveStage = null;
    }

    protected override void OnClose()
    {

    }


}
