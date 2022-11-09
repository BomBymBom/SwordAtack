using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource _soundSource;
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioClip[] _clips;
    public bool _isSoundMute = false;
    public bool _isMusicMute = false;
    public bool _isVibrateOff = false;
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(int i)
    {
        _soundSource.PlayOneShot(_clips[i]);
    }

    public void MuteUnmuteSound()
    {
        _soundSource.mute = !_soundSource.mute;
        _isSoundMute = _soundSource.mute;
    }
    public void MuteUnmuteMusic()
    {
        _musicSource.mute = !_musicSource.mute;
        _isMusicMute = _musicSource.mute;
    }

    public void Vibration()
    {
        if (!_isVibrateOff)
        {
            Handheld.Vibrate();
            Debug.Log("vibrate");
        }
    }

    public void MuteUnmuteVibration()
    {
        _isVibrateOff = !_isVibrateOff;
    }
}
