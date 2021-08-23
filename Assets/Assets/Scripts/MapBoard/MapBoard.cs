using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard:IInputHandle
{
    private IMapBrush mCurBrush;
    private int mBrushWidth; // 笔刷宽度


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

    public void OnTouchDown(Vector3 pos)
    {
        StartDraw(pos);
    }

    public void OnTouchUp(Vector3 pos)
    {
        EndDraw(pos);
    }

    public void OnDrag(Vector3 pos)
    {
        Draw(pos);
    }

    public void OnZoom(float zoomChange)
    {
        
    }
}
