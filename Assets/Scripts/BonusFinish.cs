using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusFinish : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _inGamePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerStats>() != null)
        {
            //_inGamePanel.SetActive(false);
            //_finishPanel.SetActive(true);
            //PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);


            other.gameObject.GetComponent<PlayerStats>()._GameController.Finish();
            PlayerPrefs.SetInt("NextBonusLevel", PlayerPrefs.GetInt("NextBonusLevel") + 10);
            AudioManager.instance.PlaySound(6);
        }
    }

    public void AddBonus(float bonus)
    {
        _gameController.AddMoney(bonus);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(0);

    }
}
