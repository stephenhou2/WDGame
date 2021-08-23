using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrawer_Verticle:IMapMeshRender
{
    protected int mMapWidth; //地图宽度
    protected int mMapHeight;//地图高度
    protected float mMapCellSize;//格子边长

    protected float Tile_60 = 0; // 60度对应边长
    protected float Tile_30 = 0;// 30度对应边长

    protected Vector3 topLeft = Vector3.one;
    protected Vector3 bottomLeft = Vector3.one;
    protected Vector3 bottom = Vector3.one;
    protected Vector3 bottomRight = Vector3.one;
    protected Vector3 topRight = Vector3.one;
    protected Vector3 top = Vector3.one;

    protected Vector2 uv_center = new Vector2(0.5f, 0.5f);
    protected Vector2 uv_topLeft = new Vector2(0.0f, 1.0f);
    protected Vector2 uv_bottomLeft = new Vector2(0.0f, 0.0f);
    protected Vector2 uv_bottom = new Vector2(0.5f, 0.0f);
    protected Vector2 uv_bottomRight = new Vector2(1.0f, 0.0f);
    protected Vector2 uv_topRight = new Vector2(1.0f, 1.0f);
    protected Vector2 uv_top = new Vector2(0.5f, 1.0f);

    public void InitializeMapGridDrawer(int mapWidth, int mapHeight, float mapCellSize)
    {
        mMapWidth = mapWidth;
        mMapHeight = mapHeight;
        mMapCellSize = mapCellSize;

        Tile_60 = Mathf.Sqrt(3) / 2 * mMapCellSize;
        Tile_30 = mMapCellSize / 2;

        topLeft = new Vector3(-Tile_60, Tile_30, 0);
        bottomLeft = new Vector3(-Tile_60, -Tile_30, 0);
        bottom = new Vector3(0, -mMapCellSize, 0);
        bottomRight = new Vector3(Tile_60, -Tile_30, 0);
        topRight = new Vector3(Tile_60, Tile_30, 0);
        top = new Vector3(0, mMapCellSize, 0);

        InitializeMapTiles();
    }

    protected float GetX(int col,int row)
    {
        return (col * 2 + 1 + row % 2) * Tile_60;
    }

    protected float GetY (int row)
    {
        return (1.5f * row + 1) * mMapCellSize;
    }

    protected Vector3[,] mTileCenters;
    protected Vector3[] mTileVerts;
    protected int[] mTriangles;
    protected Vector2[] mUvs;
    protected Color[] mColors;

    protected virtual void InitializeMapTiles()
    {
    }

    /// <summary>
    /// 画mesh
    /// </summary>
    public void DrawGridMesh(MeshFilter mf)
    {
        if (!Application.isPlaying)
            return;

        if (mf == null)
            return;

        InitializeMapTiles();

        Mesh mesh = new Mesh();
        mesh.vertices = mTileVerts;
        mesh.triangles = mTriangles;
        mesh.uv = mUvs;
        mesh.colors = mColors;
        mf.mesh = mesh;
    }
}
