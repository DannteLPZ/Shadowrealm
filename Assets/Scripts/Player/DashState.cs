using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    [Header("Values")]
    [Min(1.0f)]
    [SerializeField] private float _dashDistance;
    [SerializeField] private List<SpriteRenderer> _playerRenderers;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private GameObject _dashParticles;
    [SerializeField] private LayerMask _whatIsOutOfBounds;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector2.zero;
        _core.Rigidbody.gravityScale = 0.0f;
        _playerCollider.enabled = false;
        ChangeRendererAlpha(0.0f);
        Instantiate(_dashParticles, _core.transform.position, Quaternion.identity, null);
        float direction = _core.transform.rotation.y == 0.0f ? 1.0f : -1.0f;
        RaycastHit2D hit = Physics2D.Raycast(_core.transform.position, direction * Vector2.right,
                                                _dashDistance, _whatIsOutOfBounds);
        if(hit == true)
            _core.transform.position = hit.point - 0.5f * direction * Vector2.right;
        else
            _core.transform.position += (Vector3)(_dashDistance * direction * Vector2.right);
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
    
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {
        _playerCollider.enabled = true;
        _core.Rigidbody.velocity = Vector2.zero;
        _core.Rigidbody.gravityScale = 1.0f;
        ChangeRendererAlpha(1.0f);
    }
}
