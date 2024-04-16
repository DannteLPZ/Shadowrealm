using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PursuitState : State
{
    [SerializeField] private NavigateState _navigateState;
    [SerializeField] private SearchState _searchState;
    [SerializeField] private IdleState _idleState;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _circleRadius;
    [SerializeField] private float _castDistance;
    public Transform CheckForTarget()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position + 2.0f * _circleRadius * transform.right, _circleRadius, transform.right, _castDistance);
        if (hit == true)
        {
            if (hit.transform.gameObject.layer == Mathf.Log(_playerMask, 2))
                _navigateState.SetTarget(hit.transform);
            else
                _navigateState.SetTarget(null);
        }
        else
            _navigateState.SetTarget(null);

        return _navigateState.Target;
    }

    public override void Enter()
    {
        Set(_navigateState, true);
    }

    public override void Do()
    {
        if (_stateMachine.CurrentState == _navigateState && _navigateState.Target == null)
            Set(_searchState, true);

        if(_stateMachine.CurrentState == _searchState && _navigateState.Target != null)
            Set(_navigateState, true);

        if(_stateMachine.CurrentState == _navigateState && _navigateState.Target != null && _stateMachine.CurrentState.IsComplete == true)
            Set(_idleState, true);

        if (_stateMachine.CurrentState == _searchState && _stateMachine.CurrentState.IsComplete == true ||
            _stateMachine.CurrentState == _idleState && _navigateState.Target == null)
            _isComplete = true;
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
