using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private UIManager mUIMgr;
    private ResourceMgr mResMgr;
    private GameMapEditor mMapEditor; // sceneMgr完成后要整体移到scenemgr管理
    private LuaManager mLuaMgr;

    private void Awake()
    {
        InitializeAllModules();

        DontDestroyOnLoad(this);
    }

    private void InitializeAllModules()
    {
        mUIMgr = UIManager.Ins;
        mResMgr = ResourceMgr.Ins;
        mMapEditor = GameMapEditor.Ins;
        
        mLuaMgr = LuaManager.Ins;
        mLuaMgr.CreateLuaEnv();
    }

    private void DisposeAllModules()
    {


        mLuaMgr.DisposeLuaEnv();
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
        mLuaMgr.Update(deltaTime);
        mMapEditor.OnUpdate(deltaTime);
    }
}
