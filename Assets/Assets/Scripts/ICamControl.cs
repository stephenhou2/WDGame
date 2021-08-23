using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamControl 
{
    void UpdateTouchDown();

    void UpdateTouchUp();

    void UpdateTouchDrag();

    void UpdateTouchZoom();

    void UpdateTouchZoomEnd();
}
