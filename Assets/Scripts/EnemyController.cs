using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _health = 1;
    public float _damage = 1;
    public bool _isDied;

    private void Start()
    {
        //For Sliced parts of enemy
        if (_health <= 0) EnemyDie();
    }
    public void EnemyTakeDamage(float playerDamage)
    {
        _health -= playerDamage;
        if (_health <= 0)
        {

            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        Debug.Log("Enemy Die");
        _isDied = true;
        //Destroy(GetComponent<AssasinEnemyController>());
        StartCoroutine(DestroyEnemyWithDelay());
    }   
    private IEnumerator DestroyEnemyWithDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        
        
    }
}
