using UnityEngine;

[CreateAssetMenu(fileName = "Float Event", menuName = "Scriptable Objects/Events/Float")]
public class FloatEvent : GameEvent<float>
{
    [ContextMenu("Invoke (Test Value)")]
    public void Invoke() => Invoke(_testValue);
}
