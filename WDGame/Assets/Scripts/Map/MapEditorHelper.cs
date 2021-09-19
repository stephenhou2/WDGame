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

    private static Vector2Int HexToProject_Horizontal(Vector2Int cellPos)
    {
        int col = cellPos.x;
        int row = cellPos.y - cellPos.x  / 2;
        return new Vector2Int(col, row);
    }

    private static Vector2Int HexToProject_Verticle(Vector2Int cellPos)
    {
        int col = cellPos.x - cellPos.y / 2;
        int row = cellPos.y;
        return new Vector2Int(col, row);
    }

    private static Vector2Int PointToProjectCellPos_Horizontal(Vector2 pos)
    {
        return HexToProject_Horizontal(PointToHexCellPos_Horizontal(pos));
    }

    private static Vector2Int PointToProjectCellPos_Verticle(Vector2 pos)
    {
        return HexToProject_Verticle(PointToHexCellPos_Verticle(pos));
    }

    /// <summary>
    ///  ref:MapEditor/hex
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private static Vector2Int PointToHexCellPos_Horizontal(Vector2 pos)
    {
        float x = pos.x * 2 / 3 / GameMapEditor.Ins.MapConfig.MapCellSize;
        float y = (pos.y / Mathf.Sqrt(3) + pos.x / 3) / GameMapEditor.Ins.MapConfig.MapCellSize;

        int col = Mathf.FloorToInt(x);
        int row = Mathf.FloorToInt(y);
        float dx = x - col;
        float dy = y - row;

        //Log.Error("x:{0},y:{1},col:{2},row:{3},dx:{4},dy:{5}", x, y, col, row, dx, dy);

        //Area1: dy <= 0.5f * dx + 0.5f && dy <= -dx + 1f && dy >= 2 * dx - 1
        //Area2: dy <= 2 * dx - 1 && dy <= 0.5f * dx
        //Area3: dy >= -dx + 1 && dy >= 0.5f * dx && dy <= 2 * dx
        //Area4: dy >= 0.5f * dx + 0.5f && dy > 2 * dx
        if (dy <= 0.5f * dx + 0.5f && dy <= -dx + 1f && dy >= 2 * dx - 1)
        {
            return new Vector2Int(col, row);
        }
        else if(dy <= 2 * dx - 1 && dy <= 0.5f * dx)
        {
            return new Vector2Int(col+1, row);
        }
        else if(dy >= -dx + 1 && dy >= 0.5f * dx && dy <= 2 * dx)
        {
            return new Vector2Int(col+1, row+1);
        }
        else
        {
            return new Vector2Int(col, row+1);
        }
    }

    private static Vector2Int PointToHexCellPos_Verticle(Vector3 pos)
    {
        float x = (pos.x / Mathf.Sqrt(3) + pos.y / 3) / GameMapEditor.Ins.MapConfig.MapCellSize;
        float y = pos.y * 2 / 3 / GameMapEditor.Ins.MapConfig.MapCellSize;

        int col = Mathf.FloorToInt(x);
        int row = Mathf.FloorToInt(y);
        float dx = x - col;
        float dy = y - row;

        //Log.Error("x:{0},y:{1},col:{2},row:{3},dx:{4},dy:{5}", x, y, col, row, dx, dy);
        //Area1: dy <= 0.5f * dx + 0.5f && dy <= -dx + 1f && dy >= 2 * dx - 1
        //Area2: dy <= 2 * dx - 1 && dy <= 0.5f * dx
        //Area3: dy >= -dx + 1 && dy >= 0.5f * dx && dy <= 2 * dx
        //Area4: dy >= 0.5f * dx + 0.5f && dy > 2 * dx
        if (dy <= 0.5f * dx + 0.5f && dy <= -dx + 1f && dy >= 2 * dx - 1)
        {
            return new Vector2Int(col, row);
        }
        else if (dy <= 2 * dx - 1 && dy <= 0.5f * dx)
        {
            return new Vector2Int(col + 1, row);
        }
        else if (dy >= -dx + 1 && dy >= 0.5f * dx && dy <= 2 * dx)
        {
            return new Vector2Int(col + 1, row + 1);
        }
        else
        {
            return new Vector2Int(col, row + 1);
        }
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
        float x = col / 2.0f * 3 * GameMapEditor.Ins.MapConfig.MapCellSize;
        float y = (row * 2 - col % 2) * GameMapEditor.Ins.MapConfig.MapCellSize_60;
        return new Vector2(x, y);
    }

    private static Vector2 GetCellCenterVerticle(int col, int row)
    {
        float x = (col * 2 - row % 2) * GameMapEditor.Ins.MapConfig.MapCellSize_60;
        float y = row / 2.0f * 3 * GameMapEditor.Ins.MapConfig.MapCellSize;
        return new Vector2(x, y);
    }   

    public static  bool IsValidMapPos(int x,int y)
    {
        return x >= 0 && x < GameMapEditor.Ins.MapConfig.MapWidth && y >= 0 && y < GameMapEditor.Ins.MapConfig.MapHeight;
    }

    public static Vector3 TransformScreenPosToWorldPos(Camera cam,Vector2 tousPos,float howfar)
    {
        if (cam == null)
            return -Vector3.one;

        return cam.ScreenToWorldPoint(new Vector3(tousPos.x, tousPos.y, howfar));
    }
}
