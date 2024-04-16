using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private IdleState _idleState;
    [SerializeField] private NavigateState _navigateState;

    private void ChangeDirection()
    {
        float currentRotation = _core.transform.rotation.y;
        float newRotation = currentRotation == 0.0f ? 180.0f : 0.0f;
        _core.transform.rotation = Quaternion.Euler(0.0f, newRotation ,0.0f);
        Set(_navigateState, true);
    }

    public override void Enter()
    {
        ChangeDirection();
    }

    public override void Do()
    {
        if (_stateMachine.CurrentState == _navigateState)
        {
            if (_navigateState.IsComplete == true)
            {
                Set(_idleState, true);
                _core.Rigidbody.velocity = new(0.0f, _core.Rigidbody.velocity.y);
            }
        }
        else
        {
            if (_stateMachine.CurrentState.RunningTime >= 1.0f)
            {
                ChangeDirection();
            }
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }
}
