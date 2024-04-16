using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<T> : MonoBehaviour
{
    [SerializeField] private List<GameEvent<T>> _gameEvents;
    [SerializeField] private UnityEvent<T> _valueEvents;
    [SerializeField] private UnityEvent _triggerEvents;

    protected void OnEnable()
    {
        foreach (GameEvent<T> gameEvent in _gameEvents)
            gameEvent.Register(this);
    }

    protected void OnDisable()
    {
        foreach (GameEvent<T> gameEvent in _gameEvents)
            gameEvent.Deregister(this);
    }

    public void Rise(T value)
    {
        if (value != null)
            _valueEvents?.Invoke(value);
        _triggerEvents?.Invoke();
    }
}
