using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoordHelper : MonoBehaviour
{
    public bool SHOW_COORD;

    private void DrawHorizontalGrid()
    {
        float size = GameMapEditor.Ins.setting.MapCellSize;

        for (int col = 0; col < 10; col++)
        {
            Vector3 offset = new Vector3(
                3.0f / 2 * col * size,
                -Mathf.Sqrt(3) / 2 * col * size,
                0);

            Vector3 start = offset;
            Vector3 end = new Vector3(0, 10 * Mathf.Sqrt(3) * size, 0) + offset;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, end);
        }

        for (int row = 0; row < 10; row++)
        {
            Vector3 offset = new Vector3(0, row * Mathf.Sqrt(3) * size, 0);

            Vector3 start = offset;
            Vector3 end = new Vector3(15 * size, -5 * Mathf.Sqrt(3) * size, 0) + offset;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(start, end);
        }
    }

    private void DrawVerticleGrid()
    {
        float size = GameMapEditor.Ins.setting.MapCellSize;

        for (int col = 0; col < 10; col++)
        {
            Vector3 offset = new Vector3(
                Mathf.Sqrt(3) * col * size,
                0,
                0);

            Vector3 start = offset;
            Vector3 end = new Vector3(-5*Mathf.Sqrt(3) * size, 15 * size , 0) + offset;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, end);
        }

        for (int row = 0; row < 10; row++)
        {
            Vector3 offset = new Vector3(
                -Mathf.Sqrt(3) / 2 * row * size,
                3.0f/2 * row * size,  
                0);

            Vector3 start = offset;
            Vector3 end = new Vector3(10 * Mathf.Sqrt(3) * size, 0, 0) + offset;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(start, end);
        }
    }


    private void OnDrawGizmos()
    {
        if (GameMapEditor.Ins == null)
            return;

        if (!SHOW_COORD)
            return;

        if(GameMapEditor.Ins.setting.CellDirection == MapCellDirection.Horizontal)
        {
            DrawHorizontalGrid();
        }
        else if(GameMapEditor.Ins.setting.CellDirection == MapCellDirection.Verticle)
        {
            DrawVerticleGrid();
        }

    }
}
