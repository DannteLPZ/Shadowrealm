using System.Collections;
using UnityEngine;

public class BossAttackState : State
{
    [SerializeField] private FlamePillarPool _flamePillarPool;
    [SerializeField] private AnimationClip _stateAnimation;
    [SerializeField] private float _pillarSpacing;

    private int _numberOfPillars = 3;

    public override void Enter()
    {
        BossCore bossCore = (BossCore)_core;
        _numberOfPillars = bossCore.BattlePhase * 2;
        _core.Animator.Play(_stateAnimation.name);
        StartCoroutine(FlamePillars());
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

    private IEnumerator FlamePillars()
    {
        for (int i = 0; i < _numberOfPillars; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 pillarPosition = _core.transform.position + (i + 1) * _pillarSpacing * _core.transform.right;

            GameObject flamePillar = _flamePillarPool.RequestFlamePillar();
            flamePillar.transform.position = pillarPosition;
        }

        _isComplete = true;
    }

}
