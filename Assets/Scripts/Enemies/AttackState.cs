using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    [Header("Events")]
    [SerializeField] private StringEvent _attackSFXEvent;

    [Header("Parameters")]
    [SerializeField] private string _attackSFXName;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector3.zero;
        _core.Animator.Play(_stateAnimation.name);
        _attackSFXEvent.Invoke(_attackSFXName);
    }

    public override void Do()
    {
        if (RunningTime >= 1.0f)
            _isComplete = true;
    }
    public override void FixedDo()
    {
        
    }
    public override void Exit()
    {

    }
}
