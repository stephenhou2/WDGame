using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapEditor : MonoBehaviour
{
    public static GameMapEditor Ins;
    public MapEditorSettings setting;

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

    private void Awake()
    {
        GameMapEditor.Ins = this;
        mDrawerDic = new Dictionary<int, GameMapDrawer>();
        setting = new MapEditorSettings(); // 下面的组件初始化之前必须先初始化配置

        mCamControl = new MapEditorCameraControl(Camera.main,camMaxHeight,camMinHeight,zoomSpeed,moveSpeed);
        mInputControl = new MapEditorInputControl();
        mRecordMgr = new MapEditRecordManager(5);
        DataMgr = new MapEditorDataMgr();
        mMapBorad = new MapBoard();

        IntializeDrawers();

        mMapBorad.SetMapBrush(new ObstacleBrush());

        mInputControl.RegisterInputHandle(mCamControl);
        mInputControl.RegisterInputHandle(mMapBorad);

        UIManager.Ins.OpenPanel<Panel_MapEditor>("UI/MapEditor/Panel_MapEditor");
    }

    private void IntializeDrawers()
    {
        Transform GROUD_DRAWER = transform.Find(MapDefine.MAP_GROUD_DRAWER_PATH);
        if(GROUD_DRAWER != null)
        {
            mGridDrawer = new GameMapDrawer();
            mGridDrawer.InitializeMapGridDrawer(GROUD_DRAWER.gameObject, new MapGridDrawer_Horizontal(), new MapGridDrawer_Verticle());
            RegisterDrawer(MapDefine.MapDrawer_Ground, mGridDrawer);
        }

        Transform OBSTACLE_DRAWER = transform.Find(MapDefine.MAP_OBSTACLE_DRAWER_PATH);
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
            Log.Error("RegisterDrawer Error,drawer is null!");
            return;
        }

        if(!mDrawerDic.ContainsKey(type))
            mDrawerDic.Add(type, drawer);
    }

    public void LoadStageMap(int stageId)
    {
        DataMgr.LoadMapData(stageId);
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

    void Update()
    {
        if(mInputControl != null)
        {
            mInputControl.InputControlUpdate();
        }
    }

}
