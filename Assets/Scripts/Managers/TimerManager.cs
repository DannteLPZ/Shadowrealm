using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private StringEvent _timerEvent;
    private float _elapsedTime;
    private bool _isRunning;
    private void Start()
    {
        _isRunning = true;
    }
    private void Update()
    {
        if (_isRunning == false) return;
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt( _elapsedTime / 60 );
        int seconds = Mathf.FloorToInt( _elapsedTime % 60 );
        _timerEvent.Invoke("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public void StopTimer() => _isRunning = false;
}
