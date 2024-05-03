using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStepsSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _stepSound;

    public void PlayStepSound()
    {
        _audioSource.PlayOneShot(_stepSound);
    }
}
