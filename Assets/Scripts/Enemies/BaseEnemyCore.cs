using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyCore : Core
{
    [SerializeField] private PatrolState _patrolState;
    [SerializeField] private PursuitState _pursuitState;

    private bool _hasToPursuit;
    private void Start()
    {
        SetupInstances();
        _stateMachine.Set(_patrolState);
    }

    private void Update()
    {
        if (_stateMachine.CurrentState.IsComplete == true)
        {
            if (_stateMachine.CurrentState == _pursuitState)
                _stateMachine.Set(_patrolState);
        }

        if (_stateMachine.CurrentState == _patrolState)
        {
            if (_hasToPursuit == true)
                _stateMachine.Set(_pursuitState, true);
        }
        _stateMachine.CurrentState.DoBranch();
    }

    private void FixedUpdate()
    {
        _hasToPursuit = _pursuitState.CheckForTarget() != null;
        _stateMachine.CurrentState.FixedDoBranch();
    }
}
