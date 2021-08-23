using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapBrush
{
    MapDefine.MapBrushType GetBrushType();   
    void StartDraw(Vector3 pos,int brushWidth);
    void Draw(Vector3 pos,int brushWidth);
    void EndDraw(Vector3 pos,int brushWidth);
    void StartClear(Vector3 pos,int brushWidth);
    void Clear(Vector3 pos,int brushWidth);
    void EndClear(Vector3 pos,int brushWidth);
}
