using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;

    private Sounds[] playingSounds;

    public Sounds[] sounds;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.name.StartsWith("M_") == true)
                s.source.outputAudioMixerGroup = _musicGroup;
            else if (s.name.StartsWith("SFX_") == true)
                s.source.outputAudioMixerGroup = _sfxGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Play a sound 
    /// </summary>
    /// <param name="name">ID name of the sound that will be played</param>
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Stop();
    }

    public void StopMusic()
    {
        Sounds s = Array.Find(sounds, sound => sound.name.StartsWith("M_") && sound.source.isPlaying == true);

        if (s == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("No Music Playing");
#endif
            return;
        }
        s.source.Stop();
    }

    public void StopAllSFX()
    {
        //Create a list with the current playing sounds effects and stop them.
        playingSounds = sounds.Where(t => t.source.outputAudioMixerGroup == _sfxGroup).Where(p => p.source.isPlaying).ToArray();
        if (playingSounds.Length == 0) return;
        foreach (Sounds sound in playingSounds)
            sound.source.Stop();
    }

    public void ResumeAllSFX()
    {
        if (playingSounds.Length == 0) return;
        foreach (Sounds sound in playingSounds)
            sound.source.Play();
    }
}
