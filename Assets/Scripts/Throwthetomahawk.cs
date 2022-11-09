using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwthetomahawk : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed;
    private TouchControls _touchControls;
    private bool _firstTapIsEnded = false;

    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        _touchControls.TomagawkControl.Touched.started += ctx => TomagawkThrow(ctx);

    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    //private void FixedUpdate()
    //{
    //    _touchControls.TomagawkControl.Touched.started += ctx => TomagawkThrow(ctx);
    //}

    public void TomagawkThrow(InputAction.CallbackContext context)
    {
        if (_firstTapIsEnded)
        {
            _gameController._gameIsStarted = true;
            Vector2 touchPosition = context.ReadValue<Vector2>();
            Vector3 touchPositionV3 = new Vector3(touchPosition.x, touchPosition.y, 0);
            touchPositionV3 += Camera.main.transform.forward * 10f;
            Vector3 throwPosition = Camera.main.ScreenToWorldPoint(touchPositionV3);

            GameObject tempBullet = Instantiate(_bullet, _bullet.transform.position + transform.position, transform.rotation) as GameObject;
            Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
            tempBullet.transform.LookAt(throwPosition);
            tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * _bulletSpeed);
            tempRigidBodyBullet.AddTorque(tempRigidBodyBullet.transform.up * _bulletSpeed * 300);
            Destroy(tempBullet, 3.5f);
        }
        else _firstTapIsEnded = true;
    }

}


