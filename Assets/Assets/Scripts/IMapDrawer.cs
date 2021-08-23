using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapDrawer
{
    void StartDraw(Vector3 pos);
    void Draw(Vector3 pos);
    void EndDraw(Vector3 pos);
}
