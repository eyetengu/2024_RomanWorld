using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cameraSelection;
    int _cameraID;


    void Start()
    {
        ClearAllCameras();
        DisplaySelectedCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        { 
            _cameraID++;

            if (_cameraID > _cameraSelection.Length - 1)
                _cameraID = 0;

            ClearAllCameras();
            DisplaySelectedCamera();
        }
    }

    void ClearAllCameras()
    {
        foreach (var cam in _cameraSelection)
            cam.m_Priority = 10;
    }

    void DisplaySelectedCamera()
    {
        _cameraSelection[_cameraID].m_Priority = 20;

    }
}
