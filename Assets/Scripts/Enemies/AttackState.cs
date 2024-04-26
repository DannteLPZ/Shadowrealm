using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector3.zero;
        _core.Animator.Play(_stateAnimation.name);
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
