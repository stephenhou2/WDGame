using UnityEngine;

namespace GameEngine
{
    public class GameManager:MonoBehaviour
    {
        private CoroutineManager _coroutineMgr;           // 协程管理器
        private UIManager _UIMgr;                                      // UI管理器
        private ResourceMgr _resMgr;                                // 资源加载管理器
        private LuaManager _luaMgr;                                 // lua脚本管理器
        private GameSceneManager _sceneMgr;             // 场景管理器
        private CameraManager _cameraMgr;                 // 相机管理器
        private InputManager _inputMgr;                         // 输入控制管理器

        private void Awake()
        {
            InitializeAllModules();
        }

        private void InitializeAllModules()
        {
            _coroutineMgr = CoroutineManager.Ins;
            _UIMgr = UIManager.Ins;
            _resMgr = ResourceMgr.Ins;
            _cameraMgr = CameraManager.Ins;
            _inputMgr = InputManager.Ins;

            _sceneMgr = GameSceneManager.Ins;
            _sceneMgr.InitializeSceneManager();

            _luaMgr = LuaManager.Ins;
            _luaMgr.CreateLuaEnv();

            TableProto.DataTables.CreateDataTables();
            var cfg = TableProto.DataTables.Ins.GetTestSheet(1);
            Log.Logic(LogLevel.Normal, "name:{0}", cfg.Name);
        }

        private void DisposeAllModules()
        {
            _UIMgr.DisposeUIManager();
            _luaMgr.DisposeLuaEnv();
            _resMgr.DisposeResourceMgr();
            _coroutineMgr.DisposeCoroutineManager();
            _inputMgr.DisposeInputManager();
            _cameraMgr.DisposeCameraManager();
        }

        private void Start()
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
            _luaMgr.Update(deltaTime);
            _UIMgr.Update(deltaTime);
            _cameraMgr.Update(deltaTime);
        }

        private void LateUpdate()
        {
            float deltaTime = GetDeltaTime();

            _resMgr.LateUpdate(deltaTime);
            _sceneMgr.OnLateUpdate(deltaTime);
            _inputMgr.LateUpdate(deltaTime);
            _luaMgr.LateUpdate(deltaTime);
            _UIMgr.LateUpdate(deltaTime);
            _cameraMgr.LateUpdate(deltaTime);
        }

        private void OnDestroy()
        {
        
        }
    }

}
