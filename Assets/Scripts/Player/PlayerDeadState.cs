using System.Collections;
using UnityEngine;

public class PlayerDeadState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    [Header("Values")]
    [SerializeField] private Collider2D _playerCollider;

    [Header("Events")]
    [SerializeField] private BoolEvent _gameOverEvent;
    [SerializeField] private StringEvent _gameOverSFXEvent;

    private bool _hasDied;
    public override void Enter()
    {
        
    }

    public override void Do()
    {
        if(_core.GroundSensor.IsGrounded == true && _hasDied == false)
        {
            _core.Animator.Play(_stateAnimation.name);
            _core.Rigidbody.velocity = Vector3.zero;
            _core.Rigidbody.gravityScale = 0.0f;
            _playerCollider.enabled = false;
            _hasDied = true;
            StartCoroutine(DeathScreenCoroutine());
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }

    private IEnumerator DeathScreenCoroutine()
    {
        _gameOverSFXEvent.Invoke("SFX_GameOver");
        yield return new WaitForSeconds(1.0f);
        _gameOverEvent.Invoke(false);
    }
}