using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private UIManager mUIMgr;
    private ResourceMgr mResMgr;
    private GameMapEditor mMapEditor;

    private void Awake()
    {
        mUIMgr = UIManager.Ins;
        mResMgr = ResourceMgr.Ins;
        mMapEditor = GameMapEditor.Ins;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        mMapEditor.OnSceneEnter();
    }

    private float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    private void Update()
    {
        float deltaTime = GetDeltaTime();
        mUIMgr.Update(deltaTime);
        mMapEditor.OnUpdate(deltaTime);
    }
}
