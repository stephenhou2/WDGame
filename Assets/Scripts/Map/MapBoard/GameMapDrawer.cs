using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapDrawer
{
    private IMapMeshRender mHorizontalDrawer;
    private IMapMeshRender mVerticleDrawer;

    private IMapMeshRender mDrawer;
    private MeshFilter mMf;


    public void InitializeMapGridDrawer(GameObject drawer,IMapMeshRender horizontalDrawer, IMapMeshRender verticleDrawer)
    {
        if(drawer != null)
        {
            mMf = drawer.GetComponent<MeshFilter>();
        }
        mHorizontalDrawer = horizontalDrawer;
        mVerticleDrawer = verticleDrawer;
    }

    /// <summary>
    /// 画mesh
    /// </summary>
    public void MapDrawMesh()
    {
        if (!Application.isPlaying)
        {
            Log.Error("DrawGridMesh-please call draw mesh in playing mode!");
            return;
        }

        if (mMf == null)
        {
            Log.Error("DrawGridMesh-Target drawer does not has component MeshFilter");
            return;
        }

        if (GameMapEditor.Ins.setting.CellDirection == MapCellDirection.Horizontal)
        {
            mDrawer = mHorizontalDrawer;
        }
        else if (GameMapEditor.Ins.setting.CellDirection == MapCellDirection.Verticle)
        {
            mDrawer = mVerticleDrawer;
        }

        if (mDrawer == null)
            return;

        mDrawer.InitializeMapGridDrawer(GameMapEditor.Ins.setting.MapWidth, GameMapEditor.Ins.setting.MapHeight, GameMapEditor.Ins.setting.MapCellSize);

        mDrawer.DrawGridMesh(mMf);
    }
}
