using UnityEngine;

public class PlayerDeadState : State
{
    [Header("Animation")]
    [SerializeField] private AnimationClip _stateAnimation;

    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
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
}