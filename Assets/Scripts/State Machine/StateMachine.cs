using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;
    public State CurrentState => _currentState;

    public void Set(State newState, bool forceReset = false)
    {
        if (_currentState != newState || forceReset)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Initialize(this);
            _currentState.Enter();
        }
    }
}
