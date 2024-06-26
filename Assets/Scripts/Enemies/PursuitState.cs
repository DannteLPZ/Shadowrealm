using UnityEngine;

public class PursuitState : State
{
    [Header("States")]
    [SerializeField] private NavigateSensorState _investigateState;
    [SerializeField] private NavigateSensorState _chaseState;
    [SerializeField] private SearchState _searchState;
    [SerializeField] private IdleState _idleState;
    [SerializeField] private AttackState _rangedAttack;
    [SerializeField] private AttackState _slashAttack;

    [Header("Values")]
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _blocksVisionMask;
    [SerializeField] private float _circleRadius;
    [SerializeField] private float _castDistance;
    [SerializeField] private float _attackRange;

    public Transform CheckForTarget()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position + 2.0f * _circleRadius * transform.right, 
                                                _circleRadius, transform.right, _castDistance, _blocksVisionMask);
        if (hit == true)
        {
            if (hit.transform.gameObject.layer == Mathf.Log(_playerMask, 2))
            {
                _investigateState.SetTarget(hit.transform);
                _chaseState.SetTarget(hit.transform);
            }
            else
            {
                _investigateState.SetTarget(null);
                _chaseState.SetTarget(null);
            }
        }
        else
        {
            _investigateState.SetTarget(null);
            _chaseState.SetTarget(null);
        }

        return _chaseState.Target;
    }

    public override void Enter()
    {
        Set(_investigateState, true);
    }

    public override void Do()
    {
        if(_chaseState.Target != null)
        {
            float distanceToTarget = Vector2.Distance(_core.transform.position, _chaseState.Target.position);
            switch (_stateMachine.CurrentState)
            {
                case NavigateSensorState:
                    if(_stateMachine.CurrentState.IsComplete == true && distanceToTarget > _attackRange)
                    {
                        Set(_idleState, true);
                        return;
                    }
                    if (distanceToTarget > _castDistance / 2.0f && distanceToTarget <= _castDistance)
                        Set(_investigateState, false);
                    else if(distanceToTarget < _castDistance / 2.0f && distanceToTarget > _attackRange)
                        Set(_chaseState, false);
                    else if(distanceToTarget <= _attackRange && distanceToTarget > 0.3f)
                        Set(_rangedAttack, true);
                    else if(distanceToTarget <= 0.2f)
                        Set(_slashAttack, true);
                    break;
                case IdleState:
                    if (distanceToTarget <= _attackRange && distanceToTarget > 0.3f)
                        Set(_rangedAttack, true);
                    else if (distanceToTarget <= 0.2f)
                        Set(_slashAttack, true);
                    else if (_chaseState.CheckForObstacle() == false)
                        Set(_chaseState, true);
                    break;
                case SearchState: 
                    Set(_investigateState, true);
                    break;
                case AttackState:
                    if (_stateMachine.CurrentState.IsComplete == true)
                        Set(_idleState, true);
                    break;
            }
        }
        else
        {
            switch(_stateMachine.CurrentState)
            {
                case IdleState:
                    _isComplete = true;      break;
                case NavigateSensorState:
                    Set(_searchState, true); break;
                case SearchState:
                    if (_stateMachine.CurrentState.IsComplete == true)
                        _isComplete = true;
                        break;
                case AttackState:
                    if (_stateMachine.CurrentState.IsComplete == true)
                        Set(_searchState, true);
                    break;
            }
        }
    }
    public override void FixedDo()
    {
        CheckForTarget();
    }
    public override void Exit()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + 2.0f*_circleRadius * transform.right, _circleRadius);
    }
}
