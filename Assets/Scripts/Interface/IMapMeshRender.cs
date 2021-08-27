using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapMeshRender
{
    void DrawGridMesh(MeshFilter mf);

    void InitializeMapGridDrawer(int mapWidth, int mapHeight, float tileSideLength);
}
