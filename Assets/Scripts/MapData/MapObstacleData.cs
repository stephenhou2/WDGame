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

    public void InitializeMapObstacleData(int mapId,int mapWidth,int mapHeight)
    {
        string filePath = PathHelper.GetMapObsFilePath(mapId);

        if(FileHelper.FileExist(filePath))
        {
            mMapObstacleData = LoadObsDataWithData(FileHelper.ReadAllBytes(filePath),mapWidth,mapHeight);
        }
        else
        {
              mMapObstacleData = new byte[GameMapEditor.Ins.setting.MapWidth, GameMapEditor.Ins.setting.MapHeight];
        }
    }

    private byte[,] LoadObsDataWithData(byte[] data,int mapWidth,int mapHeight)
    {
        if(data.Length < mapWidth * mapHeight)
        {
            Log.Error("LoadObsDataWithData Failed,data length not equal! data length:{0},mapWidth:{1},mapHeight:{2}", data.Length, mapWidth, mapHeight);
            return null;
        }

        byte[,] obsData = new byte[mapWidth, mapHeight];
        for(int col = 0;col < mapWidth;col++)
        {
            for(int row = 0;row < mapHeight;row ++)
            {
                int index = col + row * mapWidth;
                obsData[col, row] = data[index];
            }
        }

        return obsData;
    }
}
