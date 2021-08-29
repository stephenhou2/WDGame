﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridDrawer_Verticle : MapDrawer_Verticle
{
    protected override void InitializeMapTiles()
    {
        mTileCenters = new Vector3[mMapWidth, mMapHeight];
        mTileVerts = new Vector3[mMapWidth * mMapHeight * 7];
        mUvs = new Vector2[mMapWidth * mMapHeight * 7];
        mTriangles = new int[mMapWidth * mMapHeight * 18];

        for (int col = 0; col < mMapWidth; col++)
        {
            for (int row = 0; row < mMapHeight; row++)
            {
                Vector3 center = MapEditorHelper.GetCellCenter(col, row, MapCellDirection.Verticle);
                mTileCenters[col, row] = center;

                // 顶点
                int ver_index = (col + row * mMapWidth) * 7;
                mTileVerts[ver_index] = center;
                mTileVerts[ver_index + 1] = center + topLeft;
                mTileVerts[ver_index + 2] = center + bottomLeft;
                mTileVerts[ver_index + 3] = center + bottom;
                mTileVerts[ver_index + 4] = center + bottomRight;
                mTileVerts[ver_index + 5] = center + topRight;
                mTileVerts[ver_index + 6] = center + top;

                //uv
                mUvs[ver_index] = uv_center;
                mUvs[ver_index + 1] = uv_topLeft;
                mUvs[ver_index + 2] = uv_bottomLeft;
                mUvs[ver_index + 3] = uv_bottom;
                mUvs[ver_index + 4] = uv_bottomRight;
                mUvs[ver_index + 5] = uv_topRight;
                mUvs[ver_index + 6] = uv_top;

                // 三角形
                int triangle_index = (col + row * mMapWidth) * 18;
                mTriangles[triangle_index] = ver_index;
                mTriangles[triangle_index + 1] = ver_index + 2;
                mTriangles[triangle_index + 2] = ver_index + 1;

                mTriangles[triangle_index + 3] = ver_index;
                mTriangles[triangle_index + 4] = ver_index + 3;
                mTriangles[triangle_index + 5] = ver_index + 2;

                mTriangles[triangle_index + 6] = ver_index;
                mTriangles[triangle_index + 7] = ver_index + 4;
                mTriangles[triangle_index + 8] = ver_index + 3;

                mTriangles[triangle_index + 9] = ver_index;
                mTriangles[triangle_index + 10] = ver_index + 5;
                mTriangles[triangle_index + 11] = ver_index + 4;

                mTriangles[triangle_index + 12] = ver_index;
                mTriangles[triangle_index + 13] = ver_index + 6;
                mTriangles[triangle_index + 14] = ver_index + 5;

                mTriangles[triangle_index + 15] = ver_index;
                mTriangles[triangle_index + 16] = ver_index + 1;
                mTriangles[triangle_index + 17] = ver_index + 6;
            }
        }
    }
}