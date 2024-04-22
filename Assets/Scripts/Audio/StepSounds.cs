using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private GameEvent<string> _gameEvent;
    public void MakeStepSound()
    {
        if(string.IsNullOrEmpty(_groundSensor.GroundType) == false)
        {
            _gameEvent.Invoke("SFX_" + _groundSensor.GroundType);
        }
    }
}
