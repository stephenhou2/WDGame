public class MapEditorDataMgr
{
    private string mMapId;
    private int mMapWidth;
    private int mMapHeight;
    private int mDirection;
    private float mCellSize;

    private MapObstacleData mObstacleData;
    private GameMapData.MapData mMapData;

    public MapEditorDataMgr()
    {
        mObstacleData = new MapObstacleData();
    }

    public void CreateMapData(string mapId,int mapWidth,int mapHeight,int direction,float cellSize)
    {
        mMapId = mapId;
        mMapWidth = mapWidth;
        mMapHeight = mapHeight;
        mDirection = direction;
        mCellSize = cellSize;

        mObstacleData.CreateNewObstacleData(mapWidth, mapHeight);
    }

    public bool LoadMapData(string mapId)
    {
        mMapData = ProtoDataHandler.LoadProtoData<GameMapData.MapData>(PathHelper.GetMapDataFilePath(mapId));
        if (mMapData == null)
        {
            Log.Logic("LoadMapData Faild,map:{0} dont exist!",mapId);
            return false;
        }

        mMapId = mapId;
        uint mapWidth = mMapData.MapWidth;
        uint mapHeight = mMapData.MapHeight;
        byte[] obsData = mMapData.Obstacles.ToByteArray();
        mObstacleData.InitializeMapObstacleData(obsData, (int)mapWidth, (int)mapHeight);
        return true;
    }

    public void SaveMapData(string mapId)
    {
        GameMapData.MapData mapData = new GameMapData.MapData();
        mapData.MapId = mMapId;
        mapData.MapWidth = (uint)mMapWidth;
        mapData.MapHeight = (uint)mMapHeight;
        mapData.Direction = (uint)mDirection;
        mapData.CellSize = mCellSize;
        ProtoDataHandler.SaveProtoData<GameMapData.MapData>(mapData,PathHelper.GetMapDataFilePath(mapId));
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
