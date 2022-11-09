using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressToNewSword : MonoBehaviour
{
    [SerializeField] GameController _gameController;

    [SerializeField] GameObject _toClaim;
    [SerializeField] GameObject _claimed;
    [SerializeField] Skins _skins;
    [SerializeField] Image _skinToClaimImage1;
    [SerializeField] Image _skinToClaimImage2;
    [SerializeField] Image _progressBar;
    [SerializeField] Text _progress;
    
    private void Start()
    {

        _skinToClaimImage1.sprite = _skins._lockedSkins[0].SkinImage.sprite;
        _skinToClaimImage2.sprite = _skins._lockedSkins[0].SkinImage.sprite;
    }

    public void AddProgress()
    {
        float prog = PlayerPrefs.GetFloat("Progress");
        prog += 20;
        PlayerPrefs.SetFloat("Progress", prog);

        _progressBar.fillAmount = prog/100;
        _progress.text = prog.ToString() +"%";

        CheckIfProgressAchieved(prog);
    }

    private void CheckIfProgressAchieved(float progress)
    {
        if(progress >= 100)
        {
            PlayerPrefs.SetInt("Progress", 0);
            _gameController.ClaimNewSkinPanel();
        }
    }

    public void ClaimSkin()
    {
        AudioManager.instance.PlaySound(1);

        _skins.ClaimNewSkin(_skins._lockedSkins[0].SkinIndex);
        _toClaim.SetActive(false);
        _claimed.SetActive(true);

    }

    public void ChangeClaimingSkinImage()
    {
        _skinToClaimImage1.sprite = _skinToClaimImage2.sprite = _skins._lockedSkins[0].SkinImage.sprite;

    }
}
