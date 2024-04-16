using UnityEngine;

public class PlayerCore : Core
{
    [Header("States")]
    [SerializeField] private PlayerMoveState _moveState;
    [SerializeField] private PlayerHealthState _healthState;
    [SerializeField] private PlayerAttackState _attackState;

    private PlayerControls _playerInputs;
    public PlayerControls PlayerInputs => _playerInputs;

    private void Awake()
    {
        //Initializes input asset
        _playerInputs = new();
        _playerInputs.Gameplay.Enable();
        //Assign this core to all children states
        SetupInstances();
        _stateMachine.Set(_moveState);
    }

    private void OnEnable() => _healthState.OnDamaged += SetHealthState;
    private void OnDisable()
    {
        _healthState.OnDamaged -= SetHealthState;
        _playerInputs.Gameplay.Disable();
    }

    private void Update()
    {
        SelectState();
        _stateMachine.CurrentState.DoBranch();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedDoBranch();
    }

    private void SelectState()
    {
        switch (_stateMachine.CurrentState)
        {
            case PlayerHealthState:
            case PlayerAttackState:
                if (_stateMachine.CurrentState.IsComplete == true)   
                    _stateMachine.Set(_moveState);
                break;
            case PlayerMoveState:
                if(_groundSensor.IsGrounded && _attackState.CheckForAttack() == true)
                    _stateMachine.Set(_attackState);
                break;
        }
    }

    private void SetHealthState() => _stateMachine.Set(_healthState, true);
}
