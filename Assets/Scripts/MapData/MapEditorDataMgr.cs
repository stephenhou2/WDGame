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

    public string GetMapId() { return mMapId; }
    public int GetMapWidth() { return mMapWidth; }
    public int GetMapHeight() { return mMapHeight; }
    public MapCellDirection GetMapDirection() { return (MapCellDirection)mDirection; }
    public float GetCellSize() { return mCellSize; }

    public void CreateMapData(string mapId,int mapWidth,int mapHeight,int direction,float cellSize)
    {
        mMapId = mapId;
        mMapWidth = mapWidth;
        mMapHeight = mapHeight;
        mDirection = direction;
        mCellSize = cellSize;
        mObstacleData.CreateNewObstacleData(mapWidth, mapHeight);
    }

    public bool HasMapData(string mapId)
    {
        string filePath = PathHelper.GetMapDataFilePath(mapId);

        return FileHelper.FileExist(filePath);
    }

    public void LoadMapData(string mapId)
    {
        if (!HasMapData(mapId))
        {
            Log.Error(ErrorLevel.Critical, "LoadMapData Faild,map:{0} dont exist!", mapId);
            return;
        }

        string filePath = PathHelper.GetMapDataFilePath(mapId);
        mMapData = ProtoDataHandler.LoadProtoData<GameMapData.MapData>(filePath);
        if (mMapData == null)
        {
            Log.Error(ErrorLevel.Critical,"LoadMapData Faild,map:{0} decode failed!", mapId);
            return;
        }

        mMapId = mapId;
        mMapWidth = (int)mMapData.MapWidth;
        mMapHeight = (int)mMapData.MapHeight;
        mDirection = (int)mMapData.Direction;
        mCellSize = mMapData.CellSize;
        byte[] obsData = mMapData.Obstacles.ToByteArray();
        mObstacleData.InitializeMapObstacleData(obsData, mMapWidth, mMapHeight);

        GameMapEditor.Ins.MapConfig.SetMapWidth(mMapWidth);
        GameMapEditor.Ins.MapConfig.SetMapHeight(mMapHeight);
        GameMapEditor.Ins.MapConfig.SetMapCellSize(mDirection);
        GameMapEditor.Ins.MapConfig.SetMapCellDirection((MapCellDirection)mDirection);
    }

    public void SaveMapData(string mapId)
    {
        GameMapData.MapData mapData = new GameMapData.MapData();
        mapData.MapId = mMapId;
        mapData.MapWidth = (uint)mMapWidth;
        mapData.MapHeight = (uint)mMapHeight;
        mapData.Direction = (uint)mDirection;
        mapData.CellSize = mCellSize;
        mapData.Obstacles = Google.Protobuf.ByteString.CopyFrom(mObstacleData.GetFormatObsData());

        ProtoDataHandler.SaveProtoData<GameMapData.MapData>(mapData,PathHelper.GetMapDataFilePath(mapId));
        Log.Logic(LogLevel.Hint, "SaveMapData success,MapId:{0},MapWidth:{1},MapHeight:{2},Direction:{3},CellSize:{4},obsLen:{5}", mMapId,mMapWidth, mMapHeight, mDirection, mCellSize, mapData.Obstacles.Length);
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
