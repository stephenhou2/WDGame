using UnityEngine;

public interface IInputControl
{
    void RegisterInputHandle(IInputHandle handle);
    void OnEnter();
    void OnExit();
    void InputControlUpdate(float deltaTime);
}
