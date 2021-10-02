using GameEngine;
using UnityEngine;

public class MapEditorCameraHandle: IInputHandle
{
    private Camera mCam;

    public void InitializeInputControl()
    {
        mCam = CameraManager.Ins.MainCam;
    }

    public void CamMove(Vector3 deltaPos)
    {
        if (!Input.GetKey(KeyCode.LeftControl))
            return;

        if (mCam != null)
        {
            mCam.transform.position -= deltaPos * SettingDefine.MapEditorMoveSpeed;
        }
    }

    public void CamZoom(float zoomChange)
    {
        if (mCam != null)
        {
            Vector3 camPos = mCam.transform.position;
            Vector3 newPos = new Vector3(camPos.x, camPos.y, camPos.z + zoomChange * SettingDefine.MapEditorMoveSpeed);
            if (newPos.z >= SettingDefine.MapEditorCamMinHeight && newPos.z <= SettingDefine.MapEditorCamMaxHeight)
            {
                mCam.transform.position = newPos;
            }
        }
    }

    public string GetHandleName()
    {
        return "MapCamControl";
    }

    public void OnDrag(Vector3 deltaPos,Vector3 touchPos)
    {
        CamMove(deltaPos);
    }

    public void OnTouchDown(Vector3 touchPos) {}

    public void OnTouchUp(Vector3 touchPos) {}

    public void OnZoom(float zoomChange)
    {
        CamZoom(zoomChange);
    }
}
