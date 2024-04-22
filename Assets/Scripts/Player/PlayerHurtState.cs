using UnityEngine;

public class PlayerHurtState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    [Header("Values")]
    [SerializeField] private float _knockbackForce;

    [Header("Events")]
    [SerializeField] private StringEvent _burnSFXEvent;

    private Vector2 _hitPosition;

    public override void Enter()
    {
        _core.Rigidbody.velocity = Vector2.zero;
        _core.Animator.Play(_stateAnimation.name);
        _burnSFXEvent.Invoke("SFX_Burn");
        Vector2 direction = ((Vector2)_core.transform.position - _hitPosition).normalized;
        _core.Rigidbody.AddForce(_knockbackForce * direction, ForceMode2D.Impulse);
    }

    public override void Do()
    {

    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }

    public void SetHitPosition(Vector2 hitPosition) => _hitPosition = hitPosition;
}
