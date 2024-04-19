using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName ="Scriptable Objects/Events/String")]
public class StringEvent : GameEvent<string>
{
    [ContextMenu("Invoke (Test Value)")]
    public void Invoke() => Invoke(_testValue);
}
