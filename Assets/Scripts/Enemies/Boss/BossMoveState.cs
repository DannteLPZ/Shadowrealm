using UnityEngine;

public class BossMoveState : State
{
    [Header("States")]
    [SerializeField] private IdleState _idleState;
    [SerializeField] private NavigateTransformState _navigateState;

    [Header("Values")]
    [SerializeField] private GameObject _fireShield;

    public override void Enter()
    {
        _stateMachine.Set(_idleState, true);
    }

    public override void Do()
    {
        switch(_stateMachine.CurrentState)
        {
            case IdleState:
                if (_stateMachine.CurrentState.RunningTime >= 1.0f)
                {
                    _stateMachine.Set(_navigateState, true);
                    _fireShield.SetActive(true);
                }
                break;
            case NavigateTransformState:
                if (_stateMachine.CurrentState.IsComplete == true)
                {
                    _isComplete = true;
                    _fireShield.SetActive(false);
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
