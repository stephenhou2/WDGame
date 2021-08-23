using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrawer:MonoBehaviour
{
    private IMapMeshRender mHorizontalDrawer;
    private IMapMeshRender mVerticleDrawer;

    private IMapMeshRender mDrawer;

    public void InitializeMapGrideDrawer(IMapMeshRender horizontalDrawer, IMapMeshRender verticleDrawer)
    {
        mHorizontalDrawer = horizontalDrawer;
        mVerticleDrawer = verticleDrawer;
    }

    /// <summary>
    /// 画mesh
    /// </summary>
    public void DrawGridMesh()
    {
        if (!Application.isPlaying)
            return;

        if (MapEditor.Ins.setting.CellDirection == MapCellDirection.Horizontal)
        {
            mDrawer = mHorizontalDrawer;
        }
        else if (MapEditor.Ins.setting.CellDirection == MapCellDirection.Verticle)
        {
            mDrawer = mVerticleDrawer;
        }

        if (mDrawer == null)
            return;

        MeshFilter mf = GetComponent<MeshFilter>();
        if(mf == null)
        {
            mf = this.gameObject.AddComponent<MeshFilter>();
        }

        mDrawer.InitializeMapGridDrawer(MapEditor.Ins.setting.MapWidth, MapEditor.Ins.setting.MapHeight, MapEditor.Ins.setting.MapCellSize);

        mDrawer.DrawGridMesh(mf);
    }
}
