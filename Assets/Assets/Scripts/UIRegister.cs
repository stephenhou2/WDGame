using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRegister : MonoBehaviour
{
    private void Start()
    {
        UIManager.Ins.PushPanel(new Panel_MapEditor(this.gameObject));
    }
}
