using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GameEngine;

namespace GameEngine
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        private Dictionary<string, IScene> mSceneMap = new Dictionary<string, IScene>();
        private IScene mCurScene;

        public void InitializeSceneManager()
        {
            mSceneMap.Add(SceneDef.LoginScene, new LoginScene());
            mSceneMap.Add(SceneDef.MapEditorScene, new MapEditorScene());
            mSceneMap.Add(SceneDef.PlotEditorScene, new PlotEditorScene());



            EmitterBus.AddListener(ModuleDef.SceneModule, "SwitchToScene", (evtArgs) =>
            {
                var args = evtArgs as GameEventArgs_String;
                if (null != args)
                {
                    SwitchToScene(args.Value);
                }
            });
        }

        public void SwitchToScene(string sceneName)
        {
            IEnumerator routine = AsyncLoadScene(sceneName);
            CoroutineManager.Ins.StartCoroutine(routine);
        }

        private IEnumerator AsyncLoadScene(string sceneName)
        {
            IScene toScene;
            if (!mSceneMap.TryGetValue(sceneName, out toScene))
            {
                Log.Error(ErrorLevel.Fatal, "SwitchToScene {0} Failed,scene not defined!", sceneName);
                yield break;
            }

            var op = SceneManager.LoadSceneAsync(sceneName);
            while (!op.isDone)
            {
                Log.Logic(LogLevel.Hint, "loading scene {0} ...... progress:{1}", sceneName, op.progress);
                yield return null;
            }

            if (mCurScene != null)
            {
                mCurScene.OnSceneExit();
            }

            mCurScene = toScene;
            mCurScene.OnSceneEnter();
        }

        public void Update(float deltaTime)
        {
            if (mCurScene != null)
            {
                mCurScene.OnSceneUpdate(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (mCurScene != null)
            {
                mCurScene.OnSceneLateUpdate(deltaTime);
            }
        }
    }
}
