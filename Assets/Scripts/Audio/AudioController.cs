using System.Collections;
using System.Collections.Generic;
using DroidDigital.PacMan.Helpers;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : Singleton<AudioController>
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _powerUpClip;

    [SerializeField] private AudioClip _dotPickupClip;

    [SerializeField] private AudioClip _startLevelClip;

    [SerializeField] private AudioClip _diedClip;

    public AudioSource M_Audio => _m_audio ?? (_m_audio = GetComponent<AudioSource>());

    private AudioSource _m_audio;


    private void Start()
    {
       // M_Audio.PlayOneShot(_startLevelClip);
    }

    #region Events
    
    public void OnLevelStart()
    {
        M_Audio.PlayOneShot(_startLevelClip);
    }

    public void PlayClip(AudioClip clipToPlay)
    {
        if(M_Audio.isPlaying)
            M_Audio.Stop();
        
        M_Audio.clip = clipToPlay;
        M_Audio.Play();
    }

    public void PlayMusic(AudioClip musicToPlay)
    {
        if(M_Audio.isPlaying)
            M_Audio.Stop();

        M_Audio.clip = musicToPlay;
        M_Audio.Play();
    }

    public void PlaySound(AudioClip soundToPlay)
    {
        M_Audio.PlayOneShot(soundToPlay);
    }
    
    public void PlayClip(ref AudioSource audioSource, AudioClip clip)
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
        
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOnShot(AudioClip clipToPlay)
    {
        M_Audio.PlayOneShot(clipToPlay);
    }

    public void PlayOnShot(ref AudioSource audioSource, AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }


    #endregion
}
