using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordHelper : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for(int col = 0;col<10;col++)
        {
            Vector3 offset = new Vector3(
                3.0f / 2  * col,
                -Mathf.Sqrt(3) / 2 * col,
                0);

            Vector3 start = offset ;
            Vector3 end = new Vector3(0, 10 * Mathf.Sqrt(3), 0) + offset;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, end);
        }

        for (int row = 0; row < 10; row++)
        {
            Vector3 offset = new Vector3(0,row * Mathf.Sqrt(3),0);

            Vector3 start = offset;
            Vector3 end = new Vector3(15, -5*Mathf.Sqrt(3), 0) + offset;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(start, end);
        }
    }
}
