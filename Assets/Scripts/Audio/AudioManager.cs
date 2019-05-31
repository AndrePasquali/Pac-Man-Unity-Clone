using System;
using System.Threading.Tasks;
using Aquiris.PacMan.Helpers;
using Characters.Enemies;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _dotPickupClips;

    [SerializeField] private AudioClip _startLevelClip;

    [SerializeField] private AudioClip _gameplayClip;

    [SerializeField] private AudioClip _playerDieClip;

    [SerializeField] private AudioClip _enemyDieClip;  

    [SerializeField] private AudioClip _powerUpClip;

    [SerializeField] private AudioClip _normalSpeedClip;
    [SerializeField] private AudioClip _moderateSpeedClip;
    [SerializeField] private AudioClip _fastSpeedClip;
    [SerializeField] private AudioClip _superFastClip;
    [SerializeField] private AudioClip _ultraFastClip;
    
    public AudioSource[] M_Audio => _m_audio ?? (_m_audio = GetComponents<AudioSource>());

    private AudioSource[] _m_audio;

    private int _index;

    #region Events
    
    public void OnLevelStart()
    {
        M_Audio[0].PlayOneShot(_startLevelClip);
    }

    public void OnGameplay()
    {
        M_Audio[0].clip = _gameplayClip;
        M_Audio[0].loop = true;
        M_Audio[0].Play();
    }

    public void OnPlayerPowerUp()
    {
        if(M_Audio[0].isPlaying)
            M_Audio[0].Stop();
        
        M_Audio[0].clip = _powerUpClip;
        M_Audio[0].Play();
    }

    public void OnPlayerDie()
    {
        if(_playerDieClip != null)
            M_Audio[0].PlayOneShot(_playerDieClip);
    }

    public void OnEnemyDie()
    {
        if(_enemyDieClip != null)
        M_Audio[0].PlayOneShot(_enemyDieClip);
    }

    public void OnNormalSpeed()
    {
        M_Audio[0].clip = _normalSpeedClip;
        M_Audio[0].Play();
    }

    public void OnModerateSpeed()
    {
        M_Audio[0].clip = _moderateSpeedClip;
        M_Audio[0].Play();
    }

    public void OnFastSpeed()
    {
        M_Audio[0].clip = _fastSpeedClip;
        M_Audio[0].Play();
    }
    
    public void OnSuperFastSpeed()
    {
        M_Audio[0].clip = _superFastClip;
        M_Audio[0].Play();
    }

    public void OnUltraFastSpeed()
    {
        M_Audio[0].clip = _ultraFastClip;
        M_Audio[0].Play();
    }

    public void PlayClip(AudioClip clipToPlay)
    {
        if(M_Audio[0].isPlaying)
            M_Audio[0].Stop();
        
        M_Audio[0].clip = clipToPlay;
        M_Audio[0].Play();
    }

    public void PlayMusic(AudioClip musicToPlay)
    {
        if(M_Audio[0].isPlaying)
            M_Audio[0].Stop();

        M_Audio[0].clip = musicToPlay;
        M_Audio[0].Play();
    }

    public void PlaySound(AudioClip soundToPlay)
    {
        M_Audio[0].PlayOneShot(soundToPlay);
    }
    
    public void PlayClip(ref AudioSource audioSource, AudioClip clip)
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
        
        audioSource.clip = clip;
        audioSource.Play();
    }

    public async void PlayEatingClip()
    {
        var audio = M_Audio[1];
        
        if(audio.isPlaying) return;
        
        if (audio.clip != _dotPickupClips)
            audio.clip = _dotPickupClips;

        audio.Play();

        await Task.Delay(TimeSpan.FromSeconds(0.3F));
        
        audio.Pause();

    }

    public void PlayByEnemySpeed(EnemySpeed currentSpeed)
    {
        switch (currentSpeed)
        {
                case EnemySpeed.Low: OnNormalSpeed(); break;
                    case EnemySpeed.Normal: OnNormalSpeed(); break;
                        case EnemySpeed.Moderate: OnModerateSpeed(); break;
                            case EnemySpeed.Fast: OnFastSpeed(); break;
                                case EnemySpeed.SuperFast: OnSuperFastSpeed(); break;
                                    case EnemySpeed.UltraFast: OnUltraFastSpeed(); break;
        }
    }


    public void PlayOnShot(AudioClip clipToPlay)
    {
        M_Audio[0].PlayOneShot(clipToPlay);
    }

    public void PlayOnShot(ref AudioSource audioSource, AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }

    public void StopAll()
    {
        M_Audio[0].Stop();
    }

    #endregion
}
