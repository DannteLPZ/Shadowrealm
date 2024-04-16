using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private AnimationClip _stateAnimation;
    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
    }

    public override void Do()
    {
        if (_core.GroundSensor.IsGrounded == false)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }
}
