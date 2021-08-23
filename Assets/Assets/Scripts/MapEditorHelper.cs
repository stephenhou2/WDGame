using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapEditorHelper
{

    // reference
    // https://indienova.com/indie-game-development/hex-grids-reference/#iah-1
    public static Vector2Int GetCellPos(Vector2 pos, MapCellDirection direction)
    {
        switch (direction)
        {
            case MapCellDirection.Horizontal:
                return PointToProjectCellPos_Horizontal(pos);
            case MapCellDirection.Verticle:
                return PointToProjectCellPos_Verticle(pos);
            default:
                return Vector2Int.zero;
        }
    }

    private static Vector2Int HexToProject(Vector2Int cellPos)
    {
        //col = x
        //row = z + (x + (x & 1)) / 2

        return new Vector2Int(cellPos.x, cellPos.y + (cellPos.x + (cellPos.x & 1)) / 2);
    }

    private static Vector2Int PointToProjectCellPos_Horizontal(Vector2 pos)
    {
        return HexToProject(PointToHexCellPos_Horizontal(pos));
    }

    private static Vector2Int PointToProjectCellPos_Verticle(Vector2 pos)
    {
        return HexToProject(PointToHexCellPos_Verticle(pos));
    }

    private static Vector2Int PointToHexCellPos_Horizontal(Vector2 pos)
    {
        //x' = x + y / Mathf.Sqrt(3)
        //y' = y * 2 / Mathf.Sqrt(3)

        int col = Mathf.FloorToInt((pos.x  + pos.y / Mathf.Sqrt(3)) / MapEditor.Ins.setting.MapCellSize);
        int row = Mathf.FloorToInt(pos.y * 2 / Mathf.Sqrt(3) / MapEditor.Ins.setting.MapCellSize);
        return new Vector2Int(col, row);
    }

    private static Vector2Int PointToHexCellPos_Verticle(Vector3 pos)
    {
        //function pixel_to_hex(x, y):
        //    q = (x * sqrt(3) / 3 - y / 3) / size
        //    r = y * 2 / 3 / size
        //    return hex_round(Hex(q, r))

        int col = Mathf.FloorToInt((pos.x * Mathf.Sqrt(3) / 3 - pos.y / 3) / MapEditor.Ins.setting.MapCellSize);
        int row = Mathf.FloorToInt(pos.y * 2 / 3 / MapEditor.Ins.setting.MapCellSize);
        return new Vector2Int(col, row);
    }

    public static Vector2 GetCellCenter(int col,int row, MapCellDirection direction)
    {
        switch (direction)
        {
            case MapCellDirection.Horizontal:
                return GetCellCenterHorizontal(col, row);
            case MapCellDirection.Verticle:
                return GetCellCenterVerticle(col, row);
            default:
                return Vector2.zero;
        }
    }

    private static Vector2 GetCellCenterHorizontal(int col,int row)
    {
        float x = (col / 2.0f * 3 + 1) * MapEditor.Ins.setting.MapCellSize;
        float y = (col % 2 + (row * 2 + 1)) * MapEditor.Ins.setting.MapCellSize_60;
        return new Vector2(x, y);
    }

    private static Vector2 GetCellCenterVerticle(int col, int row)
    {
        float x = (col * 2 + 1 + row % 2) * MapEditor.Ins.setting.MapCellSize_60;
        float y = (1.5f * row + 1) * MapEditor.Ins.setting.MapCellSize;
        return new Vector2(x, y);
    }   

    public static  bool IsValidMapPos(int x,int y)
    {
        return x >= 0 && x < MapEditor.Ins.setting.MapWidth && y >= 0 && y < MapEditor.Ins.setting.MapHeight;
    }

    public static Vector3 TransformScreenPosToWorldPos(Camera cam,Vector2 tousPos,float howfar)
    {
        if (cam == null)
            return -Vector3.one;

        return cam.ScreenToWorldPoint(new Vector3(tousPos.x, tousPos.y, howfar));
    }
}
