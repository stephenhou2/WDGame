using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEngine;
using TMPro;

public delegate void SelectImageCardDelegate(int index);

public class PlotEditSelectImageWindow : UIPanel
{
    private string BtnBackgroundClick = "_BACKGROUND_CLICK";
    private string BtnClose = "_BACKGROUND/_TOP_BAR/_BTN_CLOSE";
    private string InputSearch = "_BACKGROUND/_IMG_SEARCH";
    private string BtnSeach = "_BACKGROUND/_IMG_SEARCH/_BTN_SEARCH";
    private string ImgContainer = "_BACKGROUND/_IMG_SCROLLVIEW/Viewport/_IMG_CONTAINER";

    private Button _btn_background;
    private Button _btn_close;
    private TMP_InputField _input_field_search;
    private Button _btn_search;
    private GameObject _rt_img_container;


    public override string GetPanelLayerPath()
    {
        return UIPathDef.UI_LAYER_TOP_DYNAMIC;
    }

    protected override void BindUINodes()
    {
        BindButtonNode(ref _btn_background, BtnBackgroundClick,()=>
        {
            UIManager.Ins.ClosePanel<PlotEditSelectImageWindow>();
        });

        BindButtonNode(ref _btn_close, BtnClose, () =>
        {
            UIManager.Ins.ClosePanel<PlotEditSelectImageWindow>();
        });

        BindInputFieldNode(ref _input_field_search, InputSearch, "", OnSearchValueChanged);
        BindButtonNode(ref _btn_search, BtnSeach, OnSearchClick);
        BindNode(ref _rt_img_container, ImgContainer);
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void OnOpen(Dictionary<string,object> openArgs)
    {
        ShowAllSprites();

        EmitterBus.AddListener(ModuleDef.UIModule, "SelectImageCard", (GameEventArgs args) =>
         {
             GameEventArgs_Int intArgs = args as GameEventArgs_Int;
             if(intArgs != null)
             {
                 _selected_index = intArgs.Value;
                 for (int i = 0; i < _allCards.Length; i++)
                 {
                     _allCards[i].RefreshSelected(_selected_index);
                 }
             }
         });
    }

    protected override void OnClose()
    {
        
    }

    public override void CustomClear()
    {
        for (int i=0;i<_allCards.Length; i++)
        {
            UIManager.Ins.RemoveControl(this, _allCards[i]);
        }
    }

    private int _selected_index = -1;
    private PlotEditSelectImageCard[] _allCards;

    private void RefreshSelectedStatus()
    {
        for(int i=0;i<_allCards.Length;i++)
        {
            _allCards[i].RefreshSelected(_selected_index);
        }
    }

    private void ShowAllSprites()
    {
        if(PlotEditDataCenter.Ins.AllSprites != null)
        {
            _allCards = new PlotEditSelectImageCard[PlotEditDataCenter.Ins.AllSprites.Length];
            for(int i=0;i<PlotEditDataCenter.Ins.AllSprites.Length;i++)
            {
                int index = i;
                UIManager.Ins.AddControl<PlotEditSelectImageCard>(this, "Prefab/_IMG_CARD", _rt_img_container, null, (UIEntity ui) =>
                {
                    PlotEditSelectImageCard card = ui as PlotEditSelectImageCard;
                    if(card != null)
                    {
                        string path = PlotEditDataCenter.Ins.AllSprites[index];
                        card.InitImageCard(path, index);
                        _allCards[index] = card;
                    }
                });
            }
        }
    }

    private void OnSearchValueChanged(string v)
    {

    }

    private void OnSearchClick()
    {

    }
}
