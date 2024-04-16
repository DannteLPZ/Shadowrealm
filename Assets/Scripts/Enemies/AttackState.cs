using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    [Header("Values")]
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private LayerMask _layerToDamage;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackDuration;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector3.zero;
        _core.Animator.Play(_stateAnimation.name);
        _attackCollider.enabled = true;
        ContactFilter2D filter = new()
        {
            useLayerMask = true,
            layerMask = _layerToDamage
        };
        List<Collider2D> results = new();
        Physics2D.OverlapCollider(_attackCollider, filter, results);
        if (results.Count > 0)
        {
            foreach (Collider2D collider in results)
            {
                GameObject objectHit = collider.transform.parent.gameObject;
                ITakeDamage health = objectHit.GetComponentInChildren<ITakeDamage>();
                health?.TakeDamage(_attackDamage, _core.transform.position);
            }
        }

    }

    public override void Do()
    {
        if (RunningTime >= 1.0f)
            _isComplete = true;
    }
    public override void FixedDo()
    {
        
    }
    public override void Exit()
    {
        _attackCollider.enabled = false;
    }
}
