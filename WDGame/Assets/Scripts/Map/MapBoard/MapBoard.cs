using GameEngine;
using UnityEngine;

public class MapBoard:IInputHandle
{
    private IMapBrush mCurBrush;
    private int mBrushWidth; // 笔刷宽度
    private IMapBrush _obstacleBrush;

    private bool mCanBrush = true;

    public MapBoard()
    {
        _obstacleBrush = new ObstacleBrush();
    }

    public void SetMapBrush(int brushType)
    {
        if(brushType == (int)MapDefine.MapBrushType.Obstacle)
        {
            mCurBrush = _obstacleBrush;
        }
        else
        {
            mCurBrush = null;
        }
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

    private void OnChangeMode(GameEventArgs args)
    {
        GameEventArgs_Int intArg = args as GameEventArgs_Int;
        if(intArg == null)
        {
            Log.Error(ErrorLevel.Normal, "OnChangeMode Erorr, event args invalid!");
            return;
        }

        SetMapBrush(intArg.Value);
    }

    private void BindEvents()
    {
        EmitterBus.AddListener(ModuleDef.MapEditorModule, "OnMapEditorChangeMode", OnChangeMode);
    }

    public void InitializeInputControl()
    {
        mCanBrush = true;
        BindEvents();
    }
}
