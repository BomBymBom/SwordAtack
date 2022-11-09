using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Image body;
    [SerializeField] private float fadeDuration = .25f;

    private void Awake() => Instance = this;

    private void Start()
    {

        body.color = new Color(body.color.r, body.color.g, body.color.b, 1.0f);

        body.DOFade(.0f, fadeDuration);
    }

    public void LoadScene(int id)
    {
        body.DOFade(1.0f, fadeDuration).OnComplete(() => SceneManager.LoadScene(id));
    }

    public void LevelManager()
    {
        int nextBonusLevel = PlayerPrefs.GetInt("NextBonusLevel");
        int _currentLevel = PlayerPrefs.GetInt("Level");

        if (_currentLevel == nextBonusLevel)
        {
            body.DOFade(1.0f, fadeDuration).OnComplete(() => SceneManager.LoadScene("BonusLevel"));
            return;
        }

        if (_currentLevel < 6)
        {
            PlayerPrefs.SetInt("PreviousScene", _currentLevel);
            LoadScene(_currentLevel);
            return;
        }
            else
            {
                int randomScene = Random.Range(1, 6); ;

                int previousScene = PlayerPrefs.GetInt("PreviousScene");

            while (randomScene == previousScene)
                {
                    randomScene = Random.Range(1, 6);
                }

            PlayerPrefs.SetInt("PreviousScene", randomScene);
            LoadScene(randomScene);
                return;
            }
    }


}
