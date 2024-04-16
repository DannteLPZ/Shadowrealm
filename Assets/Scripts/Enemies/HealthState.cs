using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : State, ITakeDamage, IDie
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private float _corpseDuration;

    [Header("Values")]
    [SerializeField] private Collider2D _enemyCollider;

    public event Action OnDamaged;
    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
    }

    public override void Do()
    {
        if(RunningTime >= _corpseDuration)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }

    public void TakeDamage(int damage, Vector2 hitPosition) => Die();
    public void Die()
    {
        OnDamaged?.Invoke();
        _enemyCollider.enabled = false;
        _core.Rigidbody.velocity = Vector3.zero;
        _core.Rigidbody.gravityScale = 0.0f;
    }
}
