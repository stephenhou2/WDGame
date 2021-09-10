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

        BitType type = new BitType(1, "TestType 1");
        BitType type2 = new BitType(2, "TestType 2");

        BitType newType = BitType.BindEventTypeBuffer( new List<BitType> { type, type2 });

        Log.Error(ErrorLevel.Hint, "newType = {0}", newType.GetEventName());
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
