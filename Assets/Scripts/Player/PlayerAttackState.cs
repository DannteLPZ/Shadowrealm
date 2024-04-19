using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    [Header("Values")]
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private LayerMask _layerToDamage;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackBufferTime;

    private PlayerCore _playerCore;

    private float _attackTimer;
    public bool _canAttack;

    private void Start()
    {
        _playerCore = (PlayerCore)_core;
        _playerCore.PlayerInputs.Gameplay.Attack.performed += InputAttack;
    }
    private void OnEnable()
    {
        if(_playerCore != null)
        {
            _playerCore.PlayerInputs.Gameplay.Attack.performed += InputAttack;
        }
    }
    private void OnDisable()
    {
        _playerCore.PlayerInputs.Gameplay.Attack.performed -= InputAttack;
    }

    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
        StopAllCoroutines();
        _canAttack = false;
    }

    public override void Do()
    {
        _core.Rigidbody.velocity *= 0.99f;
        if (RunningTime >= _attackDuration)
            _isComplete = true;
    }
    public override void FixedDo()
    {
        if(_attackCollider.enabled == false) return;
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
    public override void Exit()
    {
        _attackCollider.enabled = false;
    }

    private void InputAttack(InputAction.CallbackContext context)
    {
        if (PauseManager.GameIsPaused == true) return;
        _canAttack = true;
        StartAttackBuffer();
    }

    private void StartAttackBuffer()
    {
        StopAllCoroutines();
        _attackTimer = 0.0f;
        StartCoroutine(TimeAttackBuffer());
    }

    private IEnumerator TimeAttackBuffer()
    {
        while(_attackTimer < _attackBufferTime)
        {
            yield return null;
            _attackTimer += Time.deltaTime;
        }
        _canAttack = false;
    }

    public bool CheckForAttack() => _canAttack;


}
