using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandle 
{
    string GetHandleName();

    void OnTouchDown(Vector3 pos);

    void OnTouchUp(Vector3 pos);

    void OnDrag(Vector3 pos);

    void OnZoom(float zoomChange);
}
