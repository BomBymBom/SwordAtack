using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMoney : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] GameObject moneyParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sword")
        {
            Instantiate(moneyParticle, transform.position, Quaternion.identity);

            _gameController.AddMoney(5);
            Destroy(this.gameObject);
        }
    }
}
