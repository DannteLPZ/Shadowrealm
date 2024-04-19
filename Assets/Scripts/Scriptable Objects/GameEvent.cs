using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent<T> : ScriptableObject
{
    [SerializeField] protected T _testValue;
    private List<GameEventListener<T>> _listeners = new();

    public void Register(GameEventListener<T> listener)
    {
        if (_listeners.Contains(listener) == false)
            _listeners.Add(listener);
    }

    public void Deregister(GameEventListener<T> listener)
    {
        if (_listeners.Contains(listener) == true)
            _listeners.Remove(listener);
    }

    public void Invoke(T value)
    {
        foreach (GameEventListener<T> listener in _listeners)
            listener.Rise(value ?? _testValue);
    }

}
