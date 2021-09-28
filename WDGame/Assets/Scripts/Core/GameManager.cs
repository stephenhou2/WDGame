using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private UIManager mUIMgr;
    private ResourceMgr mResMgr;
    private LuaManager mLuaMgr;
    private SceneManager mSceneMgr; // 场景管理器

    private void Awake()
    {
        InitializeAllModules();

        DontDestroyOnLoad(this);
    }

    private void InitializeAllModules()
    {
        mUIMgr = UIManager.Ins;
        mResMgr = ResourceMgr.Ins;

        mSceneMgr = SceneManager.Ins;
        mSceneMgr.InitializeSceneManager();

        mLuaMgr = LuaManager.Ins;
        mLuaMgr.CreateLuaEnv();
    }

    private void DisposeAllModules()
    {
        mUIMgr.DisposeUIManager();
        mLuaMgr.DisposeLuaEnv();
        mResMgr.DisposeResourceMgr();
    }

    private void Start()
    {
        mSceneMgr.SwitchToScene(SceneDef.LoginScene);
    }

    private float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    private void Update()
    {
        float deltaTime = GetDeltaTime();

        mResMgr.Update(deltaTime);
        mSceneMgr.Update(deltaTime);
        mLuaMgr.Update(deltaTime);
        mUIMgr.Update(deltaTime);
    }

    private void LateUpdate()
    {
        float deltaTime = GetDeltaTime();

        mResMgr.LateUpdate(deltaTime);
        mSceneMgr.OnLateUpdate(deltaTime);
        mLuaMgr.LateUpdate(deltaTime);
        mUIMgr.LateUpdate(deltaTime);
    }
}
