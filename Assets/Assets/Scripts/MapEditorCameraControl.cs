using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorCameraControl: IInputHandle
{
    private Camera mCam;

    private float mMaxCamHeight;
    private float mMinCamHeight;

    private float mZoomSpeed;
    private float mMoveSpeed;

    public MapEditorCameraControl(Camera cam,float camMaxHeight,float camMinHeight,float zoomSpeed,float moveSpeed)
    {
        mCam = cam;
        mMaxCamHeight = camMaxHeight;
        mMinCamHeight = camMinHeight;

        mZoomSpeed = zoomSpeed;
        mMoveSpeed = moveSpeed;
    }

    public void CamMove(Vector3 deltaPos)
    {
        if (!Input.GetKey(KeyCode.LeftControl))
            return;

        if (mCam != null)
        {
            mCam.transform.position -= deltaPos * mMoveSpeed;
        }
    }

    public void CamZoom(float zoomChange)
    {
        if (mCam != null)
        {
            Vector3 camPos = mCam.transform.position;
            Vector3 newPos = new Vector3(camPos.x, camPos.y, camPos.z + zoomChange * mZoomSpeed);
            if (newPos.z >= mMinCamHeight && newPos.z <= mMaxCamHeight)
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
