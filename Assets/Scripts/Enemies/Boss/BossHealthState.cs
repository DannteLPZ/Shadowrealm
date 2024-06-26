﻿using System;
using System.Xml;
using UnityEngine;

public class BossHealthState : State, ITakeDamage, IDie
{
    [SerializeField] private IdleState _hurtState;
    [SerializeField] private IdleState _deadState;

    [SerializeField] private FloatEvent _healthEvent;
    [SerializeField] private StringEvent _bossHurtSFXEvent;
    [SerializeField] private BoolEvent _gameOverEvent;

    [SerializeField] private int _maxHealth;
    private int _currentHealth;


    public event Action OnDamaged;
    private bool _canTakeDamage = true;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public override void Enter()
    {
        if (_currentHealth > 0)
            Set(_hurtState, true);
        else
            Set(_deadState, true);
    }

    public override void Do()
    {
        if(_stateMachine.CurrentState == _hurtState && _stateMachine.CurrentState.RunningTime >= 1.0f)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }

    public void TakeDamage(int damage, Vector2 hitPosition)
    {
        if(_canTakeDamage == true)
        {
            _currentHealth -= 1;
            _canTakeDamage = false;
            OnDamaged?.Invoke();

            _healthEvent.Invoke((float)_currentHealth/_maxHealth);
            

            if (_currentHealth <= 0)
                Die();
            else { _bossHurtSFXEvent.Invoke("SFX_BossHurt"); }
                
        }
    }

    public void Die()
    {
        _core.Rigidbody.velocity = Vector2.zero;
        _canTakeDamage = false;
        _bossHurtSFXEvent.Invoke("SFX_EnemyDie");
        _gameOverEvent.Invoke(true);
    }

    public bool CanTakeDamage(bool canTakeDamage) => _canTakeDamage = canTakeDamage;
}