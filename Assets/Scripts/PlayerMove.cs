using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Image _pathProgress;
    [SerializeField] float _moveSpead = 5;
    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCam;
    [SerializeField] CinemachineSmoothPath _cinemachineSmoothPath;
    public bool _gameIsStarted = false;

    private void Update()
    {
        if (_gameIsStarted) MovePlayer();
    }

    private void MovePlayer()
    {
       float pathPassed = _cinemachineVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += _moveSpead * Time.deltaTime;
        _pathProgress.fillAmount = pathPassed / _cinemachineSmoothPath.PathLength;
    }


}
