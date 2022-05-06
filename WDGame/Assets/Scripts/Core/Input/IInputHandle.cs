using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandle 
{
    void InitializeInputControl();

    void OnTouchDown(Vector3 touchPos);

    void OnTouchUp(Vector3 touchPos);

    void OnDrag(Vector3 deltaPos,Vector3 touchPos);

    void OnZoom(float zoomChange);

    string GetHandleName();
}
