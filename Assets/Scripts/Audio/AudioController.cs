using DroidDigital.PacMan.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioController : Singleton<AudioController>
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _powerUpClip;

    [SerializeField] private AudioClip[] _dotPickupClips;

    [SerializeField] private AudioClip _startLevelClip;

    [SerializeField] private AudioClip _gameplayClip;

    [SerializeField] private AudioClip _playerDieClip;
    

    public AudioSource M_Audio => _m_audio ?? (_m_audio = GetComponent<AudioSource>());

    private AudioSource _m_audio;


    #region Events
    
    public void OnLevelStart()
    {
        M_Audio.PlayOneShot(_startLevelClip);
    }

    public void OnGameplay()
    {
        M_Audio.clip = _gameplayClip;
        M_Audio.loop = true;
        M_Audio.Play();
    }

    public void OnPlayerDie()
    {
        if(_playerDieClip != null)
            M_Audio.PlayOneShot(_playerDieClip);
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

    public void PlayEatingClip()
    {
        if(_dotPickupClips == null) return;
        
        var clip = _dotPickupClips[Random.Range(0, _dotPickupClips.Length)];
        M_Audio.PlayOneShot(clip);
    }

    public void PlayOnShot(AudioClip clipToPlay)
    {
        M_Audio.PlayOneShot(clipToPlay);
    }

    public void PlayOnShot(ref AudioSource audioSource, AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }

    public void StopAll()
    {
        M_Audio.Stop();
    }


    #endregion
}
