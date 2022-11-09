using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public void Play(int i)
    {
        AudioManager.instance.PlaySound(i);
    }

    public void MuteSound()
    {
        AudioManager.instance.MuteUnmuteSound();
    }

    public void MuteMusic()
    {
        AudioManager.instance.MuteUnmuteMusic();

    }

    public void MuteVibration()
    {
        AudioManager.instance.MuteUnmuteVibration();
    }
}
