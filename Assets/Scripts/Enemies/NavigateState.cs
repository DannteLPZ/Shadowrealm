using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateState : State
{
    [SerializeField] private RunState _animationState;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform _sensor;
    [SerializeField] private float _sensorDistance;
    [SerializeField] private LayerMask _whatIsGround;

    private Transform _target;

    private bool _foundObstacle;

    public Transform Target => _target;

    public override void Enter()
    {
        Set(_animationState, true);
        _foundObstacle = false;

        if (_target == null)
            _core.Rigidbody.velocity = _moveSpeed * (Vector2)_core.transform.right;        
    }

    public override void Do()
    {
        if (_foundObstacle == true)
            _isComplete = true;

        /*if(_target != null)
        {
            float direction = 90.0f * Mathf.Sign(_core.Rigidbody.velocity.x) - 90.0f;
            _core.transform.rotation = Quaternion.Euler(0.0f, direction, 0.0f);
        }*/
    }
    public override void FixedDo()
    {
        bool wallInFront = Physics2D.Raycast(_sensor.position, _core.transform.right, _sensorDistance, _whatIsGround);
        bool floorBelow = Physics2D.Raycast(_sensor.position, -_core.transform.up, _sensorDistance, _whatIsGround);

        _foundObstacle = wallInFront || !floorBelow;
        if(_foundObstacle == true)
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
    public override void Exit()
    {

    }

    public void SetTarget(Transform target) => _target = target;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_sensor.position, _sensor.position + _sensorDistance * transform.right);
        Gizmos.DrawLine(_sensor.position, _sensor.position - _sensorDistance * transform.up);
    }
}
