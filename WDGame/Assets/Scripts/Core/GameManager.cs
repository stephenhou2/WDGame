using UnityEngine;
using GameEngine;

public class GameManager:MonoBehaviour
{
    private CoroutineManager            _coroutineMgr;           // 协程管理器
    private UIManager                          _UIMgr;                        // UI管理器
    private ResourceMgr                      _resMgr;                      // 资源加载管理器
    private GameSceneManager        _sceneMgr;                // 场景管理器
    private CameraManager               _cameraMgr;             // 相机管理器
    private InputManager                   _inputMgr;                 // 输入控制管理器

    private void Awake()
    {
        InitializeAllModules();

        Launch();
    }

    private void InitializeAllModules()
    {
        _coroutineMgr = CoroutineManager.Ins;
        _coroutineMgr.InitializeCoroutineManager();

        _UIMgr = UIManager.Ins;
        _resMgr = ResourceMgr.Ins;
        _cameraMgr = CameraManager.Ins;
        _inputMgr = InputManager.Ins;

        _sceneMgr = GameSceneManager.Ins;
        _sceneMgr.InitializeSceneManager();

    }

    private void DisposeAllModules()
    {
        _UIMgr.DisposeUIManager();
        _resMgr.DisposeResourceMgr();
        _coroutineMgr.DisposeCoroutineManager();
        _inputMgr.DisposeInputManager();
        _cameraMgr.DisposeCameraManager();
    }

    private void Launch()
    {
        _sceneMgr.SwitchToScene(SceneDef.LoginScene);
    }

    private float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    private void Update()
    {
        float deltaTime = GetDeltaTime();

        _coroutineMgr.Update(deltaTime);
        _resMgr.Update(deltaTime);
        _sceneMgr.Update(deltaTime);
        _inputMgr.Update(deltaTime);
        _UIMgr.Update(deltaTime);
        _cameraMgr.Update(deltaTime);
    }

    private void LateUpdate()
    {
        float deltaTime = GetDeltaTime();

        _resMgr.LateUpdate(deltaTime);
        _sceneMgr.OnLateUpdate(deltaTime);
        _inputMgr.LateUpdate(deltaTime);
        _UIMgr.LateUpdate(deltaTime);
        _cameraMgr.LateUpdate(deltaTime);
    }

    private void OnDestroy()
    {
        
    }
}
