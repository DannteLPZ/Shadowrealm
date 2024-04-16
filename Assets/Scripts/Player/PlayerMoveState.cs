using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : State
{
    [Header("Movement")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxXSpeed;
    [Range(0.0f, 0.9f)]
    [SerializeField] private float _groundDecay;
    [Range(0.1f, 0.5f)]
    [SerializeField] private float _airControlFactor;

    [Header("Dashing")]
    [Range(1.0f, 2.0f)]
    [SerializeField] private float _dashMultiplier;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    [Header("Jumping")]
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _jumpBufferTime;

    [Header("States")]
    [SerializeField] private IdleState _idleState;
    [SerializeField] private RunState _runState;
    [SerializeField] private AirState _airState;
    [SerializeField] private DashState _dashState;

    private PlayerCore _playerCore;

    private float _xInput;
    private float _jumpTimer;
    private float _dashTimer;

    private bool _hasJumped;

    private bool _canDash;
    private bool _hasDashed;

    public float XInput => _xInput;

    private void Start()
    {
        _playerCore = (PlayerCore)_core;
        _playerCore.PlayerInputs.Gameplay.Jump.performed += InputJump;
        _playerCore.PlayerInputs.Gameplay.Dash.performed += InputDash;
        _stateMachine.Set(_idleState);
    }

    private void OnDisable()
    {
        _playerCore.PlayerInputs.Gameplay.Jump.performed -= InputJump;
        _playerCore.PlayerInputs.Gameplay.Dash.performed -= InputDash;
    }

    public override void Enter()
    {
        _stateMachine.Set(_idleState, true);
    }

    public override void Do()
    {
        CheckInput();
        CheckTimer(ref _jumpTimer, ref _hasJumped, _jumpTimer >= _jumpBufferTime);
        CheckTimer(ref _dashTimer, ref _hasDashed, _dashTimer >= _dashDuration + _dashCooldown
                                                    && _core.GroundSensor.IsGrounded == true);
        SelectState();
        _stateMachine.CurrentState.Do();
    }

    public override void Exit()
    {
        _core.Animator.speed = 1.0f;
    }
    
    public override void FixedDo()
    {
        MoveWithInput();
        Jump();
        ApplyFriction();
    }

    private void SelectState()
    {
        if (_core.GroundSensor.IsGrounded == true)
        {
            if (_hasDashed == true && _dashTimer <= _dashDuration)
                _stateMachine.Set(_dashState);
            else if (_xInput == 0)
                _stateMachine.Set(_idleState);
            else
                _stateMachine.Set(_runState);
        }
        else
        {
            _stateMachine.Set(_airState);
        }
    }

    private void CheckTimer(ref float timer, ref bool runCondition, bool resetCondition)
    {
        if (runCondition == true)
        {
            timer += Time.deltaTime;
            if (resetCondition == true)
            {
                runCondition = false;
                timer = 0.0f;
            }
        }
    }

    private void ApplyFriction()
    {
        if (_core.GroundSensor.IsGrounded == true && _xInput == 0 && _core.Rigidbody.velocity.y <= 0.0f)
            _core.Rigidbody.velocity *= _groundDecay;
    }

    private void Jump()
    {
        if (_core.GroundSensor.IsGrounded == true && _hasJumped == true)
            _core.Rigidbody.velocity = new Vector2(_core.Rigidbody.velocity.x, _jumpSpeed);
    }

    private void MoveWithInput()
    {
        if (Mathf.Abs(_xInput) > 0.0f)
        {
            float increment;

            if (_core.GroundSensor.IsGrounded == true)
                increment = _xInput * _acceleration;
            else
                increment = _xInput * _acceleration * _airControlFactor;

            float newSpeed = _core.Rigidbody.velocity.x + increment;

            if (_hasDashed == true)
            {
                if (_core.GroundSensor.IsGrounded == false)
                    newSpeed = Mathf.Clamp(newSpeed, -_maxXSpeed * _dashMultiplier, _maxXSpeed * _dashMultiplier);
                else if (_dashTimer <= _dashDuration)
                    newSpeed = Mathf.Clamp(newSpeed, -_maxXSpeed * _dashMultiplier, _maxXSpeed * _dashMultiplier);
                else
                    newSpeed = Mathf.Clamp(newSpeed, -_maxXSpeed, _maxXSpeed);
            }
            else
                newSpeed = Mathf.Clamp(newSpeed, -_maxXSpeed, _maxXSpeed);
            _core.Rigidbody.velocity = new Vector2(newSpeed, _core.Rigidbody.velocity.y);

            float direction = 90.0f * Mathf.Sign(_xInput) - 90.0f;
            _core.transform.rotation = Quaternion.Euler(0.0f, direction, 0.0f);
        }
    }

    private void CheckInput()
    {
        _xInput = _playerCore.PlayerInputs.Gameplay.Movement.ReadValue<float>();
        _canDash = _core.GroundSensor.IsGrounded == true && _hasDashed == false && Mathf.Abs(_xInput) != 0.0f;
    }

    private void InputJump(InputAction.CallbackContext context) => _hasJumped = true;

    private void InputDash(InputAction.CallbackContext context)
    {
        if (_canDash == true)
            _hasDashed = true;
    }
}
