using UnityEngine;

namespace GameEngine
{
    public class GameManager:MonoBehaviour
    {
        private CoroutineManager _coroutineMgr;
        private UIManager _UIMgr;
        private ResourceMgr _resMgr;
        private LuaManager _luaMgr;
        private GameSceneManager _sceneMgr; // 场景管理器

        private void Awake()
        {
            InitializeAllModules();
        }

        private void InitializeAllModules()
        {
            _coroutineMgr = CoroutineManager.Ins;
            _UIMgr = UIManager.Ins;
            _resMgr = ResourceMgr.Ins;

            _sceneMgr = GameSceneManager.Ins;
            _sceneMgr.InitializeSceneManager();

            _luaMgr = LuaManager.Ins;
            _luaMgr.CreateLuaEnv();
        }

        private void DisposeAllModules()
        {
            _UIMgr.DisposeUIManager();
            _luaMgr.DisposeLuaEnv();
            _resMgr.DisposeResourceMgr();
            _coroutineMgr.DisposeCoroutineManager();
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
            _luaMgr.Update(deltaTime);
            _UIMgr.Update(deltaTime);
        }

        private void LateUpdate()
        {
            float deltaTime = GetDeltaTime();

            _resMgr.LateUpdate(deltaTime);
            _sceneMgr.OnLateUpdate(deltaTime);
            _luaMgr.LateUpdate(deltaTime);
            _UIMgr.LateUpdate(deltaTime);
        }

        private void OnDestroy()
        {
        
        }
    }

}
