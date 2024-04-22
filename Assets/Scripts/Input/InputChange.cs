using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class InputChange : MonoBehaviour
{
    [SerializeField] private StringEvent _onInputChange;

    private void Start() => InputSystem.onAnyButtonPress.Call(InvokeNewControl);

    private void InvokeNewControl(InputControl control) => _onInputChange.Invoke(control.device.name);
}
