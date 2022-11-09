using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _diePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _inGamePanel;
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _skinsPanel;
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _claimNewSkin;
    [SerializeField] GameObject _superPowerPanel;
    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCam;
    [SerializeField] InstantiateEnemy _enemys;

    [SerializeField] PlayerMove _playerMove;
    public float _money;
    [SerializeField] Text _moneyText;

    [SerializeField] Text _level;
    [SerializeField] Text _level1;

    public bool _gameIsStarted;
    private bool _playerIsReadyToStart = true;

    private void Awake()
    {

        _level.text = "Level "+ PlayerPrefs.GetInt("Level").ToString();
        _level1.text = PlayerPrefs.GetInt("Level").ToString();

        _money = PlayerPrefs.GetFloat("Money");
        _moneyText.text = _money.ToString();
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        int lvl = PlayerPrefs.GetInt("Level");
        if (lvl % 2 == 1 && lvl > 5 && lvl != PlayerPrefs.GetInt("NextBonusLevel") && scene.name != "Tomagawk Scene") SuperPowerOption();
        else BackToStartPanel();
    }

    private void Update()
    {
        if (_gameIsStarted && _playerIsReadyToStart ==true) StartGame();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddMoney(10000);
        }
    }

    public void StartGame()
    {
        _playerIsReadyToStart = false;

        _playerMove._gameIsStarted = true;
        
        if (_enemys)
            _enemys.ActivateAllEnemys();

        _startPanel.SetActive(false);
        _inGamePanel.SetActive(true);
    }
    public void PlayerDied()
    {
        _gameIsStarted = false;
        _playerIsReadyToStart = false;
        _playerMove._gameIsStarted = false;

        if (_enemys)
            _enemys.DeactivateAllEnemys();

        _inGamePanel.SetActive(false);
        _diePanel.SetActive(true);
        AudioManager.instance.PlaySound(2);
        AudioManager.instance.Vibration();
    }
    public void Finish()
    {

        _gameIsStarted = false;
        _playerIsReadyToStart = false;
        _playerMove._gameIsStarted = false;

        if (_enemys)
            _enemys.DeactivateAllEnemys();

        AddMoney(70);

        _inGamePanel.SetActive(false);
        _winPanel.SetActive(true);
        _canvas.GetComponent<ProgressToNewSword>().AddProgress();
        AudioManager.instance.PlaySound(6);
        //PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level") + 1);
        //if (PlayerPrefs.GetInt("Level") % 10 == 0)
        //    PlayerPrefs.SetInt("CurrentSkybox", PlayerPrefs.GetInt("CurrentSkybox") + 1);
    }

    public void Retry()
    {
        _cinemachineVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;

        _diePanel.SetActive(false);
        _winPanel.SetActive(false);
        _claimNewSkin.SetActive(false);
        _gameIsStarted = false;

        StartCoroutine(RefreshEnemys());

        

        int lvl = PlayerPrefs.GetInt("Level");
        Scene scene = SceneManager.GetActiveScene();

        if (lvl % 2 == 1 && lvl > 5 && lvl != PlayerPrefs.GetInt("NextBonusLevel") && scene.name != "Tomagawk Scene") SuperPowerOption();
        else _startPanel.SetActive(true);
        

    }

    IEnumerator RefreshEnemys()
    {
        yield return new WaitForSeconds(0.5f);
        if (_enemys)
            _enemys.InstantiateEnemys();

        _playerIsReadyToStart = true;
    }
    public void NextLevel()
    {

        _gameIsStarted = false;
        _playerIsReadyToStart = false;
        _playerMove._gameIsStarted = false;

        if (_enemys)
            _enemys.DeactivateAllEnemys();

        AudioManager.instance.PlaySound(6);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") % 10 == 0)
            PlayerPrefs.SetInt("CurrentSkybox", PlayerPrefs.GetInt("CurrentSkybox") + 1);
        SceneLoader.Instance.LevelManager();

        _playerIsReadyToStart = true;

    }

    public void SkinsPanel()
    {
        _startPanel.SetActive(false);
        _skinsPanel.SetActive(true);
        _gameIsStarted = false;
        _playerIsReadyToStart = false;
    }

    public void SettingsPanel()
    {
        _settingsPanel.SetActive(true);
        _gameIsStarted = false;
        _playerIsReadyToStart = false;
    }

    public void BackToStartPanel()
    {
        _skinsPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _superPowerPanel.SetActive(false);
        _startPanel.SetActive(true);
        _gameIsStarted = false;
        _playerIsReadyToStart = true;
    }

    public void AddMoney(float x)
    {
        _money += x;
        PlayerPrefs.SetFloat("Money", _money);
        _moneyText.text = _money.ToString();

    }

    public void ClaimNewSkinPanel()
    {
        _winPanel.SetActive(false);
        _claimNewSkin.SetActive(true);
    }

    public void SuperPowerOption()
    {
        _startPanel.SetActive(false);
        _superPowerPanel.SetActive(true);
        _gameIsStarted = false;
        _playerIsReadyToStart = false;
    }
}
