using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    [Header("Values")]
    [Min(1.0f)]
    [SerializeField] private float _dashForce;
    [SerializeField] private List<SpriteRenderer> _playerRenderers;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private GameObject _dashParticles;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector2.zero;
        _core.Rigidbody.gravityScale = 0.0f;
        _playerCollider.enabled = false;
        ChangeRendererAlpha(0.0f);
        Instantiate(_dashParticles, _core.transform.position, Quaternion.identity, null);
        float direction = _core.transform.rotation.y == 0.0f ? 1.0f : -1.0f;
        _core.Rigidbody.AddForce(_dashForce * direction * Vector2.right, ForceMode2D.Impulse);
    }

    private void ChangeRendererAlpha(float alpha)
    {
        foreach (SpriteRenderer renderer in _playerRenderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }  
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
