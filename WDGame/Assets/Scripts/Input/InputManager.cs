using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameEngine;

public class InputManager : Singleton<InputManager>
{
    private Dictionary<string, IInputControl> mInputControlDic;
    private IInputControl _curInputCtl;

    public InputManager()
    {
        mInputControlDic = new Dictionary<string, IInputControl>();
    }

    public bool HasInputControl(string inputName)
    {
        return mInputControlDic.ContainsKey(inputName);
    }

    public void RegisterInputControl(string inputName,IInputControl inputCtl)
    {
        if(inputCtl == null)
        {
            Log.Error(ErrorLevel.Critical, "RegisterInputControl failed,inputCtl \'{0}\' is null", inputName);
            return;
        }

        if (HasInputControl(inputName))
        {
            Log.Error(ErrorLevel.Normal, "RegisterInputControl failed,Re-register input \'{0}\'", inputName);
            return;
        }

        mInputControlDic.Add(inputName, inputCtl);
    }

    public void ChangeToInputControl(string inputName)
    {
        if(!mInputControlDic.TryGetValue(inputName,out IInputControl inputCtl))
        {
            Log.Error(ErrorLevel.Critical, "ChangeToInputControl failed,target input not registered: \'{0}\'", inputName);
            return;
        }

        if(_curInputCtl != null)
        {
            _curInputCtl.OnExit();
        }

        _curInputCtl = inputCtl;
        if(inputCtl != null)
        {
            inputCtl.OnEnter();
        }
    }
    

    public void Update(float deltaTime)
    {
        if(_curInputCtl != null)
        {
            _curInputCtl.InputControlUpdate(deltaTime);
        }
    }

    public void LateUpdate(float deltaTime)
    {

    }

    public void DisposeInputManager()
    {

    }
}