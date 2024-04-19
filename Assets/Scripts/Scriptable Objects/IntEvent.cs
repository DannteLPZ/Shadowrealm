using UnityEngine;

[CreateAssetMenu(fileName = "Int Event", menuName = "Scriptable Objects/Events/Int")]
public class IntEvent : GameEvent<int>
{
    [ContextMenu("Invoke (Test Value)")]
    public void Invoke() => Invoke(_testValue);
}
