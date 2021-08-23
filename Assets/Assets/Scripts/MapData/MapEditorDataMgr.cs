using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorDataMgr
{
    private MapObstacleData mObstacleData;

    public MapEditorDataMgr()
    {
        
    }

    public void LoadMapData(int stageId)
    {
        mObstacleData = new MapObstacleData(stageId);
    }

    public void UpdateObstacleData(int col,int row, byte data)
    {
        if(mObstacleData != null)
        {
            mObstacleData.SetMapObstacleDataAt(col, row, data);
        }
    }

    public byte GetObstacleDataAt(int col,int row)
    {
        if (mObstacleData != null)
        {
            return mObstacleData.GetObstacleDataAt(col, row);
        }

        return MapDefine.OBSTACLE_TYPE_EMPTY;
    }
}
