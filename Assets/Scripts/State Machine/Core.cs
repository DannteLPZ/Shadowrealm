using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Core : MonoBehaviour
{
    [Header("Core Elements")]
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected GroundSensor _groundSensor;
    [SerializeField] protected Animator _animator;

    public Rigidbody2D Rigidbody => _rigidbody;
    public GroundSensor GroundSensor => _groundSensor;
    public Animator Animator => _animator;

    //Main State Machine based on the core
    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    public void SetupInstances()
    {
        _stateMachine = new();

        State[] allStates = GetComponentsInChildren<State>();
        foreach (State state in allStates)
            state.SetupCore(this);
    }
}
