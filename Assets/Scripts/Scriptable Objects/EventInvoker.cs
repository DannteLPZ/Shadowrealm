using UnityEngine;

public class EventInvoker<T> : MonoBehaviour
{
    [SerializeField] private GameEvent<T> _gameEvent;
    [SerializeField] private T _value;

    public void Invoke() => _gameEvent.Invoke(_value);

    public void SetValue(T value) => _value = value;
}
