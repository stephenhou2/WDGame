using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBrush : IMapBrush
{
    public MapDefine.MapBrushType GetBrushType()
    {
        return MapDefine.MapBrushType.Obstacle;
    }

    public void Draw(Vector3 pos,int brushWidth)
    {
        Vector2Int cellPos = MapEditorHelper.GetCellPos(pos, MapEditor.Ins.setting.CellDirection);

        if (!MapEditorHelper.IsValidMapPos(cellPos.x, cellPos.y))
            return;

        if(null != MapEditor.Ins.DataMgr && MapEditor.Ins.DataMgr.GetObstacleDataAt(cellPos.x,cellPos.y) != MapDefine.OBSTACLE_TYPE_OBSTACLE)
        {
            MapEditor.Ins.UpdateMapObstacle(cellPos.x, cellPos.y, MapDefine.OBSTACLE_TYPE_OBSTACLE);
        }
    }

    public void EndDraw(Vector3 pos,int brushWidth)
    {
        Draw(pos, brushWidth);
        MapEditor.Ins.DrawCurMesh();
    }

    public void StartDraw(Vector3 pos,int brushWidth)
    {

    }


    public void Clear(Vector3 pos,int brushWidth)
    {

    }

    public void EndClear(Vector3 pos,int brushWidth)
    {

    }

    public void StartClear(Vector3 pos,int brushWidth)
    {

    }
}
