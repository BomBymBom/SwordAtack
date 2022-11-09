using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordMove : MonoBehaviour
{
    [SerializeField] Transform _swordTransform;
    [SerializeField] float lerpSpeed = 30.0f;
    [SerializeField] float lerpMove = 80f;

    [SerializeField] GameController _gameController;
    private TouchControls _touchControls;

    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    private void FixedUpdate()
    {
        _touchControls.SwordControl.DeltaTouch.started += ctx => SwordMoving(ctx);
    }

    Vector3 oldPosition;

    private void SwordMoving(InputAction.CallbackContext context)
    {
        _gameController._gameIsStarted = true;
        Vector2 delta = context.ReadValue<Vector2>();

        float x = transform.rotation.x + (-delta.y / 2500);
        x = Mathf.Clamp(x, -0.18f, 0.15f);

        float y = transform.rotation.y + (delta.x / 2500);
        y = Mathf.Clamp(y, -0.18f, 0.18f);

        Quaternion newQuateration = new Quaternion(x, y, transform.rotation.z, transform.rotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation,newQuateration,lerpMove * Time.deltaTime);

        //float x = transform.eulerAngles.x + (-delta.x / 100);
        //x = Mathf.Clamp(x, -20f, 20f);

        //float y = transform.eulerAngles.y + (delta.y / 100);
        //y = Mathf.Clamp(y, -20f, 20f);

        //Vector3 newQuateration = new Vector3(x, y, transform.eulerAngles.z);
        //transform.eulerAngles = newQuateration;


        Vector3 swordDelta = Vector3.zero;
        if (Vector3.Distance(_swordTransform.position, oldPosition) >= 0.025f)
        {
            swordDelta = (oldPosition - _swordTransform.position);
            oldPosition = _swordTransform.position;
        }

        SwordRotation(x, y, transform.eulerAngles, swordDelta);

    }

    Vector2 signDelta;

    private void SwordRotation(float x, float y, Vector3 InitiralPosition, Vector2 delta)
    {
        if (delta != Vector2.zero)
            signDelta = delta;

        float angle = Mathf.Atan2(signDelta.y, signDelta.x) * Mathf.Rad2Deg;
        _swordTransform.localRotation = Quaternion.Slerp(_swordTransform.localRotation,
            Quaternion.Euler(angle + (x * 100), -90 + (y * 200), 0.0f),
            lerpSpeed * Time.deltaTime);
    }
}
