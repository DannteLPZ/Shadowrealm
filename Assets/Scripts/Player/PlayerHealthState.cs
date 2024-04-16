using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthState : State, ITakeDamage
{
    [Header("Health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invincibilityTime;
    private int _currentHealth;

    [Header("States")]
    [SerializeField] private PlayerHurtState _hurtState;
    [SerializeField] private PlayerDeadState _deadState;

    public event Action OnDamaged;

    private bool _canTakeDamage = true;

    private void Start() => _currentHealth = _maxHealth;
    public override void Enter()
    {
        if(_currentHealth > 0)
            Set(_hurtState, true);
        else
            Set(_deadState, true);
    }

    public override void Do()
    {
        switch (_stateMachine.CurrentState)
        {
            case PlayerHurtState:
                if(_canTakeDamage == true)
                    _isComplete = true;
                break;
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }

    public void TakeDamage(int damage, Vector2 hitPosition)
    {
        //Disables this function from triggering multiple times
        if (_canTakeDamage == false) return;
        _canTakeDamage = false;
        _currentHealth -= damage;
        _hurtState.SetHitPosition(hitPosition);
        OnDamaged?.Invoke();
        if(_currentHealth > 0)
            StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(_invincibilityTime);
        _canTakeDamage = true;
    }
}
