using UnityEngine;

[CreateAssetMenu(fileName = "Bool Event", menuName = "Scriptable Objects/Events/Bool")]
public class BoolEvent : GameEvent<bool>
{
    [ContextMenu("Invoke (Test Value)")]
    public void Invoke() => Invoke(_testValue);
}
