public class MapEditorDataMgr
{
    private MapObstacleData mObstacleData;
    private GameMapData.MapData mMapData;

    public MapEditorDataMgr()
    {
        mObstacleData = new MapObstacleData();
        
    }

    public void CreateMapData(string mapId)
    {
        //mObstacleData = 
    }

    public bool LoadMapData(string mapId)
    {
        mMapData = ProtoDataHandler.LoadProtoData<GameMapData.MapData>(PathHelper.GetMapDataFilePath(mapId));
        if (mMapData == null)
            return false;

        int mapWidth = mMapData.MapWidth;
        int mapHeight = mMapData.MapHeight;
        mObstacleData.InitializeMapObstacleData(mapId, mapWidth, mapHeight);
        return true;
    }

    public void SaveMapData(string mapId)
    {
        if (mObstacleData != null)
        {
            
        }
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
