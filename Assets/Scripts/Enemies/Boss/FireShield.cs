using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();
    public void SetShield(bool active)
    {
        _animator.SetBool("Active", active);
    }
}
