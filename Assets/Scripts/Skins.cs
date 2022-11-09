using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skins : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] Transform _sword;
    [SerializeField] GameObject[] _swordPrefabs;
    [SerializeField] Sprite[] _swordSprites;
    [SerializeField] Image _selectedSkinImage;
    [SerializeField] Button _backButton;
    [SerializeField] Button _rewardButton;

    [SerializeField] Image[] _skinsImages;
    [SerializeField] bool[] _unLockedSins;
    public List<SkinsLocked> _lockedSkins = new List<SkinsLocked>();

    [SerializeField] Button _randomButton;
    [SerializeField] Text _randomPrice;

    private void OnEnable()
    {
        CheckIfPlayerHaveMoneyToRandom();
    }
    private void Awake()
    {
        PlayerPrefs.SetInt("0", 1);

        _randomPrice.text = PlayerPrefs.GetFloat("MoneyForRandom").ToString();

        CheckIfPlayerHaveMoneyToRandom();
        _unLockedSins = new bool[_skinsImages.Length];

        for (int i = 0; i < _skinsImages.Length; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                _skinsImages[i].color = new Color(255, 255, 255);
                _skinsImages[i].transform.parent.GetComponent<Button>().interactable = true;
                _unLockedSins[i] = true;
            }
            else
            {
                _lockedSkins.Add(new SkinsLocked(_skinsImages[i], i));
                _unLockedSins[i] = false;
            }
        }

        int selectedIndex = PlayerPrefs.GetInt("SelectedSkin");
        _selectedSkinImage.sprite = _swordSprites[selectedIndex];
        Destroy(_sword.GetChild(0).gameObject);
        Instantiate(_swordPrefabs[selectedIndex], _sword);
    }
    public void SelectingSkin(GameObject buttonSelected)
    {
        int sibilingIndex = buttonSelected.transform.GetSiblingIndex();

        _selectedSkinImage.sprite = _swordSprites[sibilingIndex];
        Destroy(_sword.GetChild(0).gameObject);
        Instantiate(_swordPrefabs[sibilingIndex], _sword);

        PlayerPrefs.SetInt("SelectedSkin", sibilingIndex);
    }

    public void BuyRandomSkin()
    {
        _randomButton.interactable = false;
        _backButton.interactable = false;
        _rewardButton.interactable = false;
        float currentMoneyForRandom = PlayerPrefs.GetFloat("MoneyForRandom");

        _gameController.AddMoney(-currentMoneyForRandom);

        currentMoneyForRandom += 200;
        PlayerPrefs.SetFloat("MoneyForRandom", currentMoneyForRandom);

        _randomPrice.text = currentMoneyForRandom.ToString();

        StartCoroutine(RandomBuy());
    }

    IEnumerator RandomBuy()
    {
        int count1 = 0;
        int count2 = 0;

        for (int i = 0; i < _unLockedSins.Length; i++)
        {

            if (!_unLockedSins[i])
            {
                _skinsImages[i].color = new Color(200, 200, 200);
                yield return new WaitForSeconds(0.3f);
                _skinsImages[i].color = new Color(0, 0, 0);

                count1++;
                AudioManager.instance.PlaySound(0);
            }
        }

        int randomNumber = Random.Range(0, count1);

        for (int i = 0; i < _unLockedSins.Length; i++)
        {
            if (!_unLockedSins[i])
            {
                AudioManager.instance.PlaySound(0);

                if (randomNumber == count2)
                {
                    _skinsImages[i].color = new Color(255, 255, 255);
                    _skinsImages[i].gameObject.GetComponentInParent<Button>().interactable = true;
                    _unLockedSins[i] = true;
                    _backButton.interactable = true;
                    _rewardButton.interactable = true;
                    PlayerPrefs.SetInt(i.ToString(), 1);

                    AudioManager.instance.PlaySound(1);

                    CheckIfPlayerHaveMoneyToRandom();
                    break;

                }
                else
                {
                    _skinsImages[i].color = new Color(200, 200, 200);
                    yield return new WaitForSeconds(0.5f);
                    _skinsImages[i].color = new Color(0, 0, 0);
                    count2++;
                }
            }
        }
    }

    public void CheckIfPlayerHaveMoneyToRandom()
    {
        if (PlayerPrefs.GetFloat("Money") >= PlayerPrefs.GetFloat("MoneyForRandom"))
        {
            _randomButton.interactable = true;
        }
        else _randomButton.interactable = false;
    }

    public void ClaimNewSkin(int i)
    {
        _skinsImages[i].color = new Color(255, 255, 255);
        _skinsImages[i].transform.parent.GetComponent<Button>().interactable = true;
        _unLockedSins[i] = true;
        PlayerPrefs.SetInt(i.ToString(), 1);
        _lockedSkins.RemoveAt(0);

        _selectedSkinImage.sprite = _swordSprites[i];
        Destroy(_sword.GetChild(0).gameObject);
        Instantiate(_swordPrefabs[i], _sword);
    }
}

public class SkinsLocked
{
    public Image SkinImage ;
    public int SkinIndex;

    public SkinsLocked(Image skinImg, int i)
    {
        this.SkinImage = skinImg;
        this.SkinIndex = i;

    }

}