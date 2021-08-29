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
            Log.Error(ErrorLevel.Critical, "CheckIsObstacle Error, invalid pos,x={0},y={1}", x, y);
            return MapDefine.OBSTACLE_TYPE_EMPTY;
        }

        if (mMapObstacleData == null)
        {
            Log.Error(ErrorLevel.Critical, "GetObstacleDataAt Error,obstacle data is null!!!");
            return MapDefine.OBSTACLE_TYPE_EMPTY;
        }

        return mMapObstacleData[x, y];
    }

    public void SetMapObstacleDataAt(int x,int y,byte data)
    {
        if (!MapEditorHelper.IsValidMapPos(x, y))
        {
            Log.Error(ErrorLevel.Fatal, "SetMapObstacleDataAt Error, invalid pos,x={0},y={1}", x, y);
            return;
        }

        mMapObstacleData[x, y] = data;
    }

    public void CreateNewObstacleData(int mapWidth,int mapHeight)
    {
        mMapObstacleData = new byte[mapWidth, mapHeight];
    }

    public void InitializeMapObstacleData(byte[] obsData,int mapWidth,int mapHeight)
    {
        if(obsData == null)
        {
            Log.Error(ErrorLevel.Critical, "InitializeMapObstacleData Error,obsData is null!!!");
            return;
        }

        mMapObstacleData = LoadObsDataWithData(obsData, mapWidth,mapHeight);
    }

    private byte[,] LoadObsDataWithData(byte[] data,int mapWidth,int mapHeight)
    {
        if(data.Length < mapWidth * mapHeight)
        {
            Log.Error(ErrorLevel.Critical, "LoadObsDataWithData Failed,data length not equal! data length:{0},mapWidth:{1},mapHeight:{2}", data.Length, mapWidth, mapHeight);
            return null;
        }

        byte[,] obsData = new byte[mapWidth, mapHeight];
        for (int row = 0; row < mapHeight; row++)
        {
            for (int col = 0; col < mapWidth; col++)
            {
                int index = col + row * mapWidth;
                if (index >= 0 && index < obsData.Length)
                {
                    obsData[col, row] = data[col + row * mapWidth];
                }
                else
                {
                    Log.Error(ErrorLevel.Fatal, "LoadObsDataWithData Error,index out of range!col={0},row={1},mapWidth={2},mapHeigh={3}", col, row, mapWidth, mapHeight);
                }
            }
        }

        return obsData;
    }
}
