using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneLoader : MonoBehaviour
{
    private void Awake()
    {

    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("NextBonusLevel")) PlayerPrefs.SetInt("NextBonusLevel", 5);
        if (!PlayerPrefs.HasKey("Progress")) PlayerPrefs.SetFloat("Progress", 0);
        if (!PlayerPrefs.HasKey("MoneyForRandom")) PlayerPrefs.SetFloat("MoneyForRandom", 1000);
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 1);
        if (!PlayerPrefs.HasKey("CurrentSkybox")) PlayerPrefs.SetInt("CurrentSkybox", 0);
        SceneLoader.Instance.LevelManager();
    }

    void Update()
    {
        
    }
}
