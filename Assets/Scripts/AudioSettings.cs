using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] GameObject _musicMute;
    [SerializeField] GameObject _musicUnMute;   
    [SerializeField] GameObject _soundsMute;
    [SerializeField] GameObject _soundsUnMute;
    [SerializeField] GameObject _vibrationMute;
    [SerializeField] GameObject _vibrationUnMute;

    private void Start()
    {
        if (AudioManager.instance._isMusicMute)
        {
            _musicMute.SetActive(true);
            _musicUnMute.SetActive(false);
        }
        if (AudioManager.instance._isSoundMute)
        {
            _soundsMute.SetActive(true);
            _soundsUnMute.SetActive(false);
        }
        if (AudioManager.instance._isVibrateOff)
        {
            _vibrationMute.SetActive(true);
            _vibrationUnMute.SetActive(false);
        }
    }
}
