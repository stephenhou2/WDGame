using UnityEngine;
using GameEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Control_BrushEdit : UIControl
{
    private Button _Button_BrushEdit;
    private GameObject _Node_Slider_Brush;
    private GameObject _Node_Image_BrushSize;
    private GameObject _Node_Text_BrushSize;

    private bool mBrushSliderShow = false;

    public override void CustomClear()
    {
        _Node_Slider_Brush = null;
        _Node_Image_BrushSize = null;
        _Node_Text_BrushSize = null;
    }

    private void OnClickBrushEdit()
    {
        mBrushSliderShow = !mBrushSliderShow;
        _Node_Slider_Brush.SetActive(mBrushSliderShow);
    }

    protected override void BindUINodes()
    {
        BindButtonNode(ref _Button_BrushEdit, "_BrushEdit", OnClickBrushEdit);
        BindNode(ref _Node_Slider_Brush, "_Slider_Brush");

        BindNode(ref _Node_Image_BrushSize, "_Image_BrushSize");
        BindNode(ref _Node_Text_BrushSize, "_Text_BrushSize");
    }

    protected override void OnClose()
    {
        
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string,object> openArgs)
    {
        InitializeBrushSlider();
    }


    /// <summary>
    /// 初始化笔刷slider
    /// </summary>
    private void InitializeBrushSlider()
    {
        RefreshSliderValue(GameMapEditor.Ins.MapConfig.BrushWidth);

        UIInterface.AddSilderChangeAction(_Node_Slider_Brush, OnSliderValueChanged);
    }

    /// <summary>
    /// 刷新笔刷大小
    /// </summary>
    /// <param name="value"></param>
    private void RefreshSliderValue(int value)
    {
        UIInterface.SetTextString(_Node_Text_BrushSize, value.ToString());
        UIInterface.SetGraphicSize(_Node_Image_BrushSize, 10 + value * 2, 10 + value * 2);
        UIInterface.SetSilderValue(_Node_Slider_Brush, value);
    }

    /// <summary>
    /// 笔刷大小改变
    /// </summary>
    /// <param name="value"></param>
    private void OnSliderValueChanged(float value)
    {
        int v = Mathf.FloorToInt(value);
        if (v != GameMapEditor.Ins.MapConfig.BrushWidth)
        {
            GameMapEditor.Ins.MapConfig.SetBrushWidth((int)value);
            RefreshSliderValue((int)value);
        }
    }
}
