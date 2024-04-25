using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    [SerializeField] private string _levelMusicName;
    [SerializeField] private BoolEvent _stopMusicEvent;
    [SerializeField] private StringEvent _playMusicEvent;

    private void Start()
    {
        _stopMusicEvent.Invoke(true);
        _playMusicEvent.Invoke(_levelMusicName);
    }
}
