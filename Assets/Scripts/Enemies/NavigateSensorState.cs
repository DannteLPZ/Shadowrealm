using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateSensorState : State
{
    [Header("Animation")]
    [SerializeField] private RunState _animationState;

    [Header("Values")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _sensor;
    [SerializeField] private float _sensorDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private SpriteRenderer _iconRenderer;

    private Transform _target;

    private bool _foundObstacle;

    public Transform Target => _target;

    public override void Enter()
    {
        Set(_animationState, true);
        if(_iconRenderer != null) _iconRenderer.enabled = true;
        _foundObstacle = false;

        if (_target == null)
            _core.Rigidbody.velocity = _moveSpeed * (Vector2)_core.transform.right;        
    }

    public override void Do()
    {
        if (_foundObstacle == true)
            _isComplete = true;
        if (_iconRenderer != null) _iconRenderer.flipX = _core.transform.rotation.y != 0.0f;
    }
    public override void FixedDo()
    {
        _foundObstacle = CheckForObstacle();
        if (_foundObstacle == true)
        {
            _core.Rigidbody.velocity = Vector2.zero;
            return;
        }
        if (_target == null)
            _core.Rigidbody.velocity = _moveSpeed * (Vector2)_core.transform.right;
        else
        {
            Vector2 direction = (_target.position - _core.transform.position).normalized;
            _core.Rigidbody.velocity = new(_moveSpeed * direction.x, _core.Rigidbody.velocity.y);
        }
    }

    public bool CheckForObstacle()
    {
        bool wallInFront = Physics2D.Raycast(_sensor.position, _core.transform.right, _sensorDistance, _whatIsGround);
        bool floorBelow = Physics2D.Raycast(_sensor.position, -_core.transform.up, _sensorDistance, _whatIsGround);

        return wallInFront || !floorBelow;
    }

    public override void Exit()
    {
        if (_iconRenderer != null) _iconRenderer.enabled = false;
    }

    public void SetTarget(Transform target) => _target = target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_sensor.position, _sensor.position + _sensorDistance * transform.right);
        Gizmos.DrawLine(_sensor.position, _sensor.position - _sensorDistance * transform.up);
    }
}
