using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private int _turnTimes;
    [SerializeField] private float _turnDelay;

    private int _currentTurnTimes;

    public override void Enter()
    {
        _core.Animator.Play(_stateAnimation.name);
        _core.Rigidbody.velocity = Vector3.zero;
        _currentTurnTimes = 0;
        StartCoroutine(SearchTarget());
    }

    public override void Do()
    {
        if(_currentTurnTimes >= _turnTimes)
            _isComplete = true;
    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator SearchTarget()
    {
        float currentRotation = _core.transform.rotation.y;
        float newRotation = currentRotation == 0.0f ? 180.0f : 0.0f;
        _core.transform.rotation = Quaternion.Euler(0.0f, newRotation, 0.0f);
        _currentTurnTimes++;
        if( _currentTurnTimes < _turnTimes)
        {
            yield return new WaitForSeconds(_turnDelay);
            StartCoroutine(SearchTarget());
        }
    }
}
