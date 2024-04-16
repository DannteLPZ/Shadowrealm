using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private float _maxXSpeed;
    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
    }

    public override void Do()
    {
        _core.Animator.speed = Utilities.MapValue(_maxXSpeed, 0.0f, 1.0f, 0.0f, 1.6f, true);
        if (_core.GroundSensor.IsGrounded == false)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {
        _core.Animator.speed = 1.0f;
    }
}
