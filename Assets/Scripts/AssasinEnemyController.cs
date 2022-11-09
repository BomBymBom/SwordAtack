using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssasinEnemyController : MonoBehaviour
{
    [SerializeField] Transform _player;
    //[SerializeField] float _moveSpeed = 5f;
    //private Rigidbody _rb;
    public float distanceBetweenObjects;
    private void Start()
    {
        _player = FindObjectOfType<Camera>().GetComponentInParent<Transform>();
        //_rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        distanceBetweenObjects = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceBetweenObjects <= 15)
        {
            Vector3 target = new Vector3(_player.position.x, transform.position.y, _player.position.z);
            transform.LookAt(target);
                
            if (distanceBetweenObjects <= 10 && distanceBetweenObjects >5.1 && !GetComponent<EnemyController>()._isDied && GetComponent<Animator>() != null) GetComponent<Animator>().SetTrigger("Run");
            
                if (distanceBetweenObjects <= 5) EnemySwordAttack();
        }

    }

    private void EnemySwordAttack()
    {
        if(GetComponent<Animator>() != null)
        GetComponent<Animator>().SetTrigger("SwordAttack");
    }
}
