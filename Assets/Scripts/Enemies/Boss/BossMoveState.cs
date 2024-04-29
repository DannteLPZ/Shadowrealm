using UnityEngine;

public class BossMoveState : State
{
    [Header("States")]
    [SerializeField] private IdleState _idleState;
    [SerializeField] private NavigateTransformState _navigateState;

    [Header("Values")]
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private BoolEvent _activateFireShield;

    public override void Enter()
    {
        _stateMachine.Set(_idleState, true);
        _activateFireShield.Invoke(true);
    }

    public override void Do()
    {
        switch(_stateMachine.CurrentState)
        {
            case IdleState:
                if (_stateMachine.CurrentState.RunningTime >= 1.0f)
                {
                    _stateMachine.Set(_navigateState, true);
                    _activateFireShield.Invoke(false);
                    _fireBall.SetActive(true);
                }
                break;
            case NavigateTransformState:
                if (_stateMachine.CurrentState.IsComplete == true)
                {
                    _isComplete = true;
                    _fireBall.SetActive(false);
                    _activateFireShield.Invoke(true);
                }
                break;
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }
}
