using UnityEngine.Audio;    
using System;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    #region Singleton class: AudioManager
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public Sound[] sounds;
    public Slider volumeSlider;


    private void Start() {
         foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        Play("Theme");
    }
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void changeVolume()
    {
        foreach (Sound s in sounds)
        {
            s.volume = volumeSlider.value; 
            s.source.volume = s.volume;
        }
    }

}
