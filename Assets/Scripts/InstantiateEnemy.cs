using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] _enemys;
    [SerializeField] GameObject[] _enemyPrefabs;
    [SerializeField] Transform[] _instantiatePoints;
    [SerializeField] bool[] _enemyUnlooked;

    public bool _gameIsStarted;

    private void Awake()
    {
        int unlockedEnemy = (PlayerPrefs.GetInt("Level") / 10) + 1;
        if (_enemyUnlooked.Length < unlockedEnemy) unlockedEnemy = _enemyUnlooked.Length;

        for(int i =0; i < unlockedEnemy; i++)
        {
            _enemyUnlooked[i] = true;
        }
        InstantiateEnemys();
    }

    public void InstantiateEnemys()
    {
        DestroyAllEnemy();

        int i = 0;
        int maxEnemyType = 0;
        _enemys = new GameObject[_instantiatePoints.Length];

        foreach (bool _enemyType in _enemyUnlooked)
        {
            if (_enemyType == true) maxEnemyType++;
        }

        foreach (Transform point in _instantiatePoints)
        {
            _enemys[i++] = Instantiate(_enemyPrefabs[Random.Range(0, maxEnemyType)], point.position, point.rotation, transform);
        }


    }
    private void DestroyAllEnemy()
    {
        if(_enemys != null)
        foreach(GameObject enemy in _enemys)
        {
            Destroy(enemy);
        }
    }

    public void ActivateAllEnemys()
    {
        foreach (GameObject enemy in _enemys)
        {
            if (enemy.TryGetComponent<MageAttack>(out MageAttack distanceAttak))
                distanceAttak._gameIsStarted = true;
        }
    }

    public void DeactivateAllEnemys()
    {
        foreach (GameObject enemy in _enemys)
        {
            if(enemy !=null)
            if (enemy.TryGetComponent<MageAttack>(out MageAttack distanceAttak))
                distanceAttak._gameIsStarted = false;
        }
    }
}
