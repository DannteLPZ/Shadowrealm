using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Core
{
    [SerializeField] private PlayerMoveState _moveState;
    [SerializeField] private PlayerHealthState _healthState;
    [SerializeField] private PlayerAttackState _attackState;

    private void OnEnable()
    {
        //_healthState.OnDamageTaken += () => { _stateMachine.Set(_healthState, true); };
    }

    private void Awake()
    {
        //Assign this core to all children states
        SetupInstances();
        _stateMachine.Set(_moveState);
    }

    private void Update()
    {
        SelectState();
        _stateMachine.CurrentState.DoBranch();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedDoBranch();
    }

    private void SelectState()
    {
        if (_stateMachine.CurrentState.IsComplete == true)
        {
            if (_stateMachine.CurrentState == _healthState)
            {
                _stateMachine.Set(_moveState);
            }
        }
    }
}
