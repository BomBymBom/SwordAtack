using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressToNewSkyBox : MonoBehaviour
{
    [SerializeField] private Image _curentMap;
    [SerializeField] private Image _nextMap;
    [SerializeField] private Sprite[] _maps;
    [SerializeField] private Image[] _lvlsProgress;
    [SerializeField] private Material[] _skyBoxes;

    void Start()
    {

        int _currentLevel = PlayerPrefs.GetInt("Level");
        int skyBoxId = PlayerPrefs.GetInt("CurrentSkybox");

        for(int i = 0; i< _currentLevel % 10; i++)
        {
            _lvlsProgress[i].color = new Color(0, 225, 0);
        }

        if (_skyBoxes.Length - 1 < skyBoxId)
        {
            skyBoxId = 0;
            PlayerPrefs.SetInt("CurrentSkybox", skyBoxId);
        }
        else
            PlayerPrefs.SetInt("CurrentSkybox", skyBoxId);

        RenderSettings.skybox = _skyBoxes[skyBoxId];

        _curentMap.sprite = _maps[skyBoxId];
        if (skyBoxId == _maps.Length) skyBoxId = -1;
        _nextMap.sprite = _maps[skyBoxId + 1];
        

    }

}
