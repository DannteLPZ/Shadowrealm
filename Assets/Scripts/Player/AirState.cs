using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private float _jumpSpeed;
    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
    }

    public override void Do()
    {
        float time = Utilities.MapValue(_core.Rigidbody.velocity.y, _jumpSpeed, -_jumpSpeed, 0.0f, 1.0f, true);
        _core.Animator.Play(_stateAnimation.name, 0, time);
        _core.Animator.speed = 0.0f;

        if (_core.GroundSensor.IsGrounded == true && _core.Rigidbody.velocity.y <= 0.0f)
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
