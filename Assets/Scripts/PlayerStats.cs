using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _health = 1;
    public float _damage = 1;
    public GameController _GameController;

    public void PlayerTakeDamage(float playerDamage)
    {
        _health -= playerDamage;
        if (_health <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        _GameController.PlayerDied();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "Finish")
    //    {
    //        _GameController.Finish();
    //    }
    //}
}
