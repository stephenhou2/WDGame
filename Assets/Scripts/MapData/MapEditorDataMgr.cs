public class MapEditorDataMgr
{
    private MapObstacleData mObstacleData;
    //private GameMapData mMapData;

    public MapEditorDataMgr()
    {
        mObstacleData = new MapObstacleData();
        
    }

    public void LoadMapData(int mapId)
    {


        mObstacleData.InitializeMapObstacleData(mapId, 10, 10);
    }

    public void SaveMapData(int mapId)
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
