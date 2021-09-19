using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapEditor : Singleton<GameMapEditor>,ISceneMgr
{
    public GameObject mMapEditorRoot;

    public MapEditorConfigs MapConfig;

    private GameMapDrawer mGridDrawer;  // 网格绘制
    private GameMapDrawer mObstacleDrawer; //阻挡格绘制

    private MapEditorInputControl mInputControl; // 输入控制
    private MapEditorCameraControl mCamControl;// 相机控制
    public MapEditorDataMgr DataMgr; //地图数据层
    private MapBoard mMapBorad;  //地图绘制

    private MapEditRecordManager mRecordMgr; // 操作回退功能

    public float zoomSpeed;
    public float moveSpeed;
    public float camMaxHeight;
    public float camMinHeight;

    private Dictionary<int, GameMapDrawer> mDrawerDic;

    private void IntializeDrawers()
    {
        Transform GROUD_DRAWER = mMapEditorRoot.transform.Find(MapDefine.MAP_GROUD_DRAWER_PATH);
        if(GROUD_DRAWER != null)
        {
            mGridDrawer = new GameMapDrawer();
            mGridDrawer.InitializeMapGridDrawer(GROUD_DRAWER.gameObject, new MapGridDrawer_Horizontal(), new MapGridDrawer_Verticle());
            RegisterDrawer(MapDefine.MapDrawer_Ground, mGridDrawer);
        }

        Transform OBSTACLE_DRAWER = mMapEditorRoot.transform.Find(MapDefine.MAP_OBSTACLE_DRAWER_PATH);
        if (OBSTACLE_DRAWER != null)
        {
            mObstacleDrawer = new GameMapDrawer();
            mObstacleDrawer.InitializeMapGridDrawer(OBSTACLE_DRAWER.gameObject, new MapObstacleDrawer_Horizontal(), new MapObstacleDrawer_Verticle());
            RegisterDrawer(MapDefine.MapDrawer_Obstacle, mObstacleDrawer);
        }
    }

    private void RegisterDrawer(int type,GameMapDrawer drawer)
    {
        if(drawer == null)
        {
            Log.Error(ErrorLevel.Critical, "RegisterDrawer Error,drawer is null!");
            return;
        }

        if(!mDrawerDic.ContainsKey(type))
            mDrawerDic.Add(type, drawer);
    }

    public void CreateNewMap(string mapId, int mapWidth, int mapHeight, int direction, float cellSize)
    {
        DataMgr.CreateMapData(mapId, mapWidth, mapHeight, direction, cellSize);
        DoDraw(MapDefine.MapDrawer_Ground);
        RefreshMapConfig();
    }

    private void RefreshMapConfig()
    {
        MapConfig.SetMapWidth(DataMgr.GetMapWidth());
        MapConfig.SetMapHeight(DataMgr.GetMapHeight());
        MapConfig.SetMapCellSize(DataMgr.GetCellSize());
        MapConfig.SetMapCellDirection(DataMgr.GetMapDirection());
    }

    public void LoadStageMap(string mapId)
    {
        DataMgr.LoadMapData(mapId);
        RefreshMapConfig();

        DoDraw(MapDefine.MapDrawer_Ground);
        DoDraw(MapDefine.MapDrawer_Obstacle);
    }

    public void UpdateMapObstacle(int col,int row, byte data)
    {
        DataMgr.UpdateObstacleData(col, row, data);
    }

    public void DoDraw(int drawerType)
    {
        if(mDrawerDic.TryGetValue(drawerType,out GameMapDrawer drawer))
        {
            drawer.MapDrawMesh();
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if(mInputControl != null)
            mInputControl.InputControlUpdate();

    }

    public void OnSceneEnter()
    {
        mMapEditorRoot = GameObject.Find(GameDefine._MAP_EDITOR);
        mDrawerDic = new Dictionary<int, GameMapDrawer>();
        MapConfig = new MapEditorConfigs(); // 下面的组件初始化之前必须先初始化配置
        mCamControl = new MapEditorCameraControl(
            Camera.main,
            SettingDefine.MapEditorCamMaxHeight,
            SettingDefine.MapEditorCamMinHeight,
            SettingDefine.MapEditorZoomSpeed,
            SettingDefine.MapEditorMoveSpeed);


        mInputControl = new MapEditorInputControl();
        mRecordMgr = new MapEditRecordManager(5);
        DataMgr = new MapEditorDataMgr();
        mMapBorad = new MapBoard();
        mMapBorad.SetMapBrush(new ObstacleBrush());
        mInputControl.RegisterInputHandle(mCamControl);
        mInputControl.RegisterInputHandle(mMapBorad);

        IntializeDrawers();
        UIManager.Ins.OpenPanel<Panel_MapEditor>("UI/MapEditor/Panel_MapEditor", null); 
    }

    public void OnSceneExit()
    {
        
    }
}
