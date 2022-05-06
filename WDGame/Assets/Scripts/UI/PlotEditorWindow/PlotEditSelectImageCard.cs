using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using GameEngine;
using TMPro;

public class PlotEditSelectImageCard : UIControl
{
    private Image _img_sprite;
    private Button _btn_select;
    private TMP_Text _text_name;
    private Image _img_name_back;

    protected override void OnOpen(Dictionary<string, object> openArgs)
    {
        _img_sprite.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(_sprite_path);
        _text_name.GetComponent<TMP_Text>().text = Path.GetFileName(_sprite_path);
        _img_name_back.GetComponent<Image>().color = deselectedColor;
    }

    public override bool CheckCanOpen(Dictionary<string, object> openArgs)
    {
        return true;
    }

    protected override void BindUINodes()
    {
        BindImageNode(ref _img_sprite, "_IMG_SPRITE");
        BindButtonNode(ref _btn_select, "", () =>
        {
        EmitterBus.Fire(ModuleDef.UIModule, "SelectImageCard", new GameEventArgs_Int(_index));
        });

        BindTextNode(ref _text_name, "_IMG_NAME");
        BindImageNode(ref _img_name_back, "_IMG_NAME_BACK");
    }


    protected override void OnClose()
    {
        
    }

    public override void CustomClear()
    {
        
    }



    private Color selectedColor = new Color(0, 0, 1, 1);
    private Color deselectedColor = new Color(0, 0, 1, 0);

    public void RefreshSelected(int selectedIndex)
    {
        if(_img_name_back != null)
            _img_name_back.GetComponent<Image>().color = _index == selectedIndex ? selectedColor : deselectedColor;
    }

    private string _sprite_path;
    private int _index;
    public void InitImageCard(string spritePath, int index)
    {
        _sprite_path = spritePath;
        _index = index;
    }

}
