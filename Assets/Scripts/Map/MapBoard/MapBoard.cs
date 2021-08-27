using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard:IInputHandle
{
    private IMapBrush mCurBrush;
    private int mBrushWidth; // 笔刷宽度

    private bool mCanBrush = true;

    public void SetMapBrush(IMapBrush brush)
    {
        mCurBrush = brush;
    }

    public void SetBrushWidth(int width)
    {
        mBrushWidth = width;
    }

    public void StartDraw(Vector3 pos)
    {
        if (mCurBrush != null)
            mCurBrush.StartDraw(pos, mBrushWidth);
    }

    public void Draw(Vector3 pos)
    {
        if (mCurBrush != null)
            mCurBrush.Draw(pos, mBrushWidth);
    }

    public void EndDraw(Vector3 pos)
    {
        if (mCurBrush != null)
            mCurBrush.EndDraw(pos, mBrushWidth);
    }


    public void Undo()
    {

    }

    public void Redo()
    {

    }

    public string GetHandleName()
    {
        return "MapBoard";
    }

    public void OnTouchDown(Vector3 touchPos)
    {
        if (Camera.main == null)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            mCanBrush = false;
            return;
        }

        Vector3 worldPos = MapEditorHelper.TransformScreenPosToWorldPos(Camera.main, touchPos, -Camera.main.transform.position.z);
        StartDraw(worldPos);
    }

    public void OnTouchUp(Vector3 touchPos)
    {
        if (Camera.main == null)
            return;

        if (!mCanBrush)
        {
            mCanBrush = true;
            return;
        }

        Vector3 worldPos = MapEditorHelper.TransformScreenPosToWorldPos(Camera.main, touchPos, -Camera.main.transform.position.z);
        EndDraw(worldPos);
    }

    public void OnDrag(Vector3 deltaPos,Vector3 touchPos)
    {
        if (Camera.main == null)
            return;

        if (!mCanBrush)
            return;

        Vector3 worldPos = MapEditorHelper.TransformScreenPosToWorldPos(Camera.main, touchPos, -Camera.main.transform.position.z);
        Draw(worldPos);
    }

    public void OnZoom(float zoomChange)
    {
        
    }
}
