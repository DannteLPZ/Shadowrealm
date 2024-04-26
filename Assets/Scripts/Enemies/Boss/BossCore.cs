using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class BossCore : Core
{
    [Header("States")]
    [SerializeField] private BossMoveState _moveState;
    [SerializeField] private BossAttackState _attackState;
    [SerializeField] private BossHealthState _healthState;
    [SerializeField] private IdleState _tiredState;

    private int _attackTimes;
    private int _battlePhase;
    public int BattlePhase => _battlePhase;

    private void OnEnable() => _healthState.OnDamaged += SetHealthState;
    private void OnDisable() => _healthState.OnDamaged -= SetHealthState;

    private void Start()
    {
        SetupInstances();
        _battlePhase = 1;
        _stateMachine.Set(_moveState);
    }

    private void Update()
    {
        if (PauseManager.GameIsPaused == true) return;
        switch (_stateMachine.CurrentState)
        {
            case BossMoveState:
                if (_stateMachine.CurrentState.IsComplete == true)
                {
                    _stateMachine.Set(_attackState, true);
                    _attackTimes++;
                }
                break;
            case BossAttackState:
                if (_stateMachine.CurrentState.IsComplete == true)
                {
                    if(_attackTimes >= _battlePhase * 2)
                    {
                        _stateMachine.Set(_tiredState, true);
                        _healthState.CanTakeDamage(true);
                    }
                    else
                        _stateMachine.Set(_moveState, true);
                }
                break;
            case BossHealthState:
                if (_stateMachine.CurrentState.IsComplete == true)
                {
                    _battlePhase++;
                    _stateMachine.Set(_moveState, true);
                }
                break;
            case IdleState:
                if (_stateMachine.CurrentState.RunningTime >= 2.0f)
                {
                    _healthState.CanTakeDamage(false);
                    _stateMachine.Set(_moveState, true);
                    _attackTimes = 0;
                }
                break;
        }
        _stateMachine.CurrentState.DoBranch();
    }

    private void SetHealthState() => _stateMachine.Set(_healthState, true);
}

