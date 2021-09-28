using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager:Singleton<SceneManager>
{
    private Dictionary<string, IScene> mSceneMap = new Dictionary<string, IScene>();
    private IScene mCurScene;



    public void InitializeSceneManager()
    {
        mSceneMap.Add(SceneDef.MapEditorScene, new MapEditorScene());

        EmitterBus.AddListener(ModuleDef.SceneMgr, "SwitchToScene", (evtArgs) =>
        {
            var args = evtArgs as GameEventArgs_String;
            if(null != args)
            {
                SwitchToScene(args.Value);
            }
        });
    }

    public void SwitchToScene(string sceneName)
    {
        if (mCurScene != null)
        {
            mCurScene.OnSceneExit();
        }

    
    }

    public void Update(float deltaTime)
    {
        if(mCurScene != null)
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
