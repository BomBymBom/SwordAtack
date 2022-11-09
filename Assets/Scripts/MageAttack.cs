using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    //[SerializeField] float _moveSpeed = 5f;
    //private Rigidbody _rb;
    public float distanceBetweenObjects;
    public bool _gameIsStarted = false;

    private void Start()
    {
        _player = FindObjectOfType<Camera>().GetComponentInParent<Transform>();
        //_rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        distanceBetweenObjects = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceBetweenObjects <= 20 && _gameIsStarted)
        {
            Vector3 target = new Vector3(_player.position.x, transform.position.y, _player.position.z);
            transform.LookAt(target);

            EnemyDistanceAttack();
        }

    }
    private void EnemyDistanceAttack()
    {
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Attack");
    }
    //Used in MageAttack Animation
    void Fire() 
    {
        AudioManager.instance.PlaySound(5);

        GameObject tempBullet = Instantiate(_bullet, _bullet.transform.position + transform.position, transform.rotation ) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * _bulletSpeed);
        tempRigidBodyBullet.AddTorque(tempRigidBodyBullet.transform.up * _bulletSpeed* 300);
        Destroy(tempBullet, 5f);
    }

}
