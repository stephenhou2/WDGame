using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObstacleData
{
    private byte[,] mMapObstacleData;

    public byte[,] GetAllObstacleData()
    {
        return mMapObstacleData;
    }

    public byte GetObstacleDataAt(int x,int y)
    {
        if(!MapEditorHelper.IsValidMapPos(x,y))
        {
            Log.Error("CheckIsObstacle Error, invalid pos,x={0},y={1}", x, y);
            return MapDefine.OBSTACLE_TYPE_EMPTY;
        }

        return mMapObstacleData[x, y];
    }

    public void SetMapObstacleDataAt(int x,int y,byte data)
    {
        if (!MapEditorHelper.IsValidMapPos(x, y))
        {
            Log.Error("SetMapObstacleDataAt Error, invalid pos,x={0},y={1}", x, y);
            return;
        }

        mMapObstacleData[x, y] = data;
    }

    public MapObstacleData(int stageId)
    {
        if (stageId == -1) //新关卡
        {
            mMapObstacleData = new byte[MapEditor.Ins.setting.MapWidth, MapEditor.Ins.setting.MapHeight];
        }
        else// 加载已有关卡数据
        {
            mMapObstacleData = new byte[MapEditor.Ins.setting.MapWidth, MapEditor.Ins.setting.MapHeight];
        }
    }
}
