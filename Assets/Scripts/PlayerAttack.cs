using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _playerDamage = 1;

    private void Start()
    {
        //_playerDamage = GetComponentInParent<PlayerStats>()._damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.Vibration();

            other.gameObject.GetComponent<EnemyController>().EnemyTakeDamage(_playerDamage);
        }
        AudioManager.instance.PlaySound(3);

    }
}
