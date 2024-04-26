using UnityEngine;

public class NavigateTransformState : State
{
    [Header("Target")]
    [SerializeField] private Transform _target1;
    [SerializeField] private Transform _target2;

    [Header("States")]
    [SerializeField] private RunState _runState;

    [Header("Values")]
    [SerializeField] private float _moveSpeed;

    private Transform _currentTarget;
    private bool _hasReachedTarget;

    private void Start()
    {
        _currentTarget = _target1;
    }

    public override void Enter()
    {
        _hasReachedTarget = false;
        _currentTarget = _currentTarget == _target1 ? _target2 : _target1;
        float distance = _currentTarget.position.x - _core.transform.position.x;
        float angle = 90.0f * Mathf.Sign(distance) - 90.0f;
        _core.transform.localRotation = Quaternion.Euler(angle * Vector3.up);
        _core.Rigidbody.velocity = new Vector2(_moveSpeed * Mathf.Sign(distance), _core.Rigidbody.velocity.y);
        _stateMachine.Set(_runState, true);
    }

    public override void Do()
    {
        if(Mathf.Abs(_core.transform.position.x - _currentTarget.position.x) <= 0.1f && _hasReachedTarget == false)
        {
            _core.Rigidbody.velocity = Vector3.zero;
            float angle = _core.transform.localRotation.y == 0.0f ? -180.0f : 0.0f;
            _core.transform.localRotation = Quaternion.Euler(angle * Vector3.up);
            _isComplete = true;
            _hasReachedTarget = true;
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {
        _hasReachedTarget = false;
    }
}
