using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Core _core;

    //State Machine from where this state was called
    protected StateMachine _parentStateMachine;

    //State Machine for this state
    protected StateMachine _stateMachine;

    protected bool _isComplete;
    protected float _startTime;
    public bool IsComplete => _isComplete;
    public float RunningTime => Time.time - _startTime;
    public State CurrentState => _stateMachine.CurrentState;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void SetupCore(Core core)
    {
        _stateMachine = new();
        _core = core;
    }

    public void Initialize(StateMachine parent)
    {
        _parentStateMachine = parent;
        _isComplete = false;
        _startTime = Time.time;
    }

    protected void Set(State newState, bool forceReset) => _stateMachine.Set(newState, forceReset);

    public void DoBranch()
    {
        Do();
        CurrentState?.DoBranch();
    }

    public void FixedDoBranch()
    {
        FixedDo();
        CurrentState?.FixedDoBranch();
    }

}
