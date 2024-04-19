using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyCore : Core
{
    [Header("States")]
    [SerializeField] private PatrolState _patrolState;
    [SerializeField] private PursuitState _pursuitState;
    [SerializeField] private HealthState _healthState;

    private bool _hasToPursuit;

    private void OnEnable() => _healthState.OnDamaged += SetHealthState;
    private void OnDisable() => _healthState.OnDamaged -= SetHealthState;

    private void Start()
    {
        SetupInstances();
        _stateMachine.Set(_patrolState);
    }

    private void Update()
    {
        if (PauseManager.GameIsPaused == true) return;
        switch (_stateMachine.CurrentState)
        {
            case PatrolState:
                if (_hasToPursuit == true)
                    _stateMachine.Set(_pursuitState, true);
                break;
            case PursuitState:
                if (_stateMachine.CurrentState.IsComplete == true)
                    _stateMachine.Set(_patrolState);
                break;
            case HealthState:
                if (_stateMachine.CurrentState.IsComplete == true)
                    Destroy(gameObject);
                break;
        }
        _stateMachine.CurrentState.DoBranch();
    }

    private void FixedUpdate()
    {
        _hasToPursuit = _pursuitState.CheckForTarget() != null;
        _stateMachine.CurrentState.FixedDoBranch();
    }

    private void SetHealthState() => _stateMachine.Set(_healthState, true);
}
