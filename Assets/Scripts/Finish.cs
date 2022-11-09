using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<PlayerStats>() != null)
        {
            other.gameObject.GetComponent<PlayerStats>()._GameController.Finish();

        }
    }
}
