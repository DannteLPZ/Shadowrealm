using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private LayerMask _layerToDamage;
    [SerializeField] private int _attackDamage;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector3.zero;
        _core.Animator.Play(_stateAnimation.name);

    }

    public override void Do()
    {
        if (RunningTime >= 2.0f)
            _isComplete = true;
    }
    public override void FixedDo()
    {
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
                Debug.Log(collider.name);
                collider.gameObject.TryGetComponent(out IHealth health);
                health?.TakeDamage(_attackDamage);
            }
        }
    }
    public override void Exit()
    {

    }
}
