using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    [Header("Values")]
    [Range(1.0f, 2.0f)]
    [SerializeField] private float _dashMultiplier;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private GameObject _dashParticles;

    public override void Enter()
    {
        _core.Rigidbody.velocity = _core.Rigidbody.velocity.x * _dashMultiplier * Vector2.right;
        _core.Rigidbody.gravityScale = 0.0f;
        _playerCollider.enabled = false;
        ChangeRendererAlpha(0.0f);
        Instantiate(_dashParticles, _core.transform.position, Quaternion.identity, null);
    }

    private void ChangeRendererAlpha(float alpha)
    {
        Color color = _playerRenderer.color;
        color.a = alpha;
        _playerRenderer.color = color;
    }

    public override void Do()
    {
        if (_core.GroundSensor.IsGrounded == false)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {
        _playerCollider.enabled = true;
        _core.Rigidbody.gravityScale = 1.0f;
        ChangeRendererAlpha(1.0f);
    }
}
