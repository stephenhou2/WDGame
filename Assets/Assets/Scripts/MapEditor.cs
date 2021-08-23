using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public static MapEditor Ins;
    public MapEditorSettings setting;

    private MapDrawer mGridDrawer;  // 网格绘制
    private MapDrawer mObstacleDrawer; //阻挡格绘制
    private MapEditorInputControl mInputControl; // 输入控制
    private MapEditorCameraControl mCamControl;// 相机控制
    public MapEditorDataMgr DataMgr; //地图数据层
    private MapBoard mMapBorad;  //地图绘制

    private MapEditRecordManager mRecordMgr; // 操作回退功能

    public float zoomSpeed;
    public float moveSpeed;
    public float camMaxHeight;
    public float camMinHeight;

    private void Awake()
    {
        MapEditor.Ins = this;
        setting = new MapEditorSettings(); // 下面的组件初始化之前必须先初始化配置

        mCamControl = new MapEditorCameraControl(Camera.main,camMaxHeight,camMinHeight,zoomSpeed,moveSpeed);
        mInputControl = new MapEditorInputControl();
        mRecordMgr = new MapEditRecordManager(5);
        DataMgr = new MapEditorDataMgr();
        mMapBorad = new MapBoard();
        mGridDrawer = transform.Find(MapDefine.MAP_GRID_DRAWER_PATH).GetComponent<MapDrawer>();
        mObstacleDrawer = transform.Find(MapDefine.MAP_OBSTACLE_DRAWER_PATH).GetComponent<MapDrawer>();
        mGridDrawer.InitializeMapGrideDrawer(new MapGridDrawer_Horizontal(),new MapGridDrawer_Verticle());
        mObstacleDrawer.InitializeMapGrideDrawer(new MapObstacleDrawer_Horizontal(),new MapObstacleDrawer_Verticle());

        mMapBorad.SetMapBrush(new ObstacleBrush());

        mInputControl.RegisterInputHandle(mCamControl);
        mInputControl.RegisterInputHandle(mMapBorad);
    }

    public void LoadStageMap(int stageId)
    {
        DataMgr.LoadMapData(stageId);
    }

    public void UpdateMapObstacle(int col,int row, byte data)
    {
        DataMgr.UpdateObstacleData(col, row, data);
    }

    public void DrawCurMesh()
    {
        mObstacleDrawer.DrawGridMesh();
    }

    public void DrawGrids()
    {
        mGridDrawer.DrawGridMesh();
    }

    void Update()
    {
        if(mInputControl != null)
        {
            mInputControl.InputControlUpdate();
        }
    }

}
