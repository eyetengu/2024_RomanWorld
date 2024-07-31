using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverIntermediate : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _rotationSpeed = 100;

    float _step;
    float _rotationStep;

    float _speedMultiplier = 1.0f;

    [SerializeField] private Animation_Manager _animManager;


    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;
        _rotationStep = _rotationSpeed * _speedMultiplier * Time.deltaTime;

        PlayerMover();
    }

   void PlayerMover()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _speedMultiplier = 2.0f;
        else
            _speedMultiplier = 1.0f;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float turn = Input.GetAxis("Mouse X");

        if (vertical < 0)
        {
            _animManager.SetWalk(false, true);
            if (_speedMultiplier > 1.0f)
                _animManager.SetRun(true, true);
        }
        else if (vertical > 0)
        {
            _animManager.SetWalk(false, false);
            if (_speedMultiplier > 3.0f)
                _animManager.SetRun(true, false);
        }
        else
            _animManager.SetIdle();

        if(horizontal < 0)
        {
            _animManager.StrafeLeft(true);
            _animManager.StrafeRight(false);
        }
        else if (horizontal > 0)
        {
            _animManager.StrafeLeft(false);
            _animManager.StrafeRight(true);
        }
        else
        {
            _animManager.StrafeLeft(false);
            _animManager.StrafeRight(false);
        }    

        //Vector3 direction = new Vector3(horizontal, 0, vertical);
        transform.Rotate(0, turn * _rotationStep, 0);

        //transform.TransformDirection(direction);
        transform.position += transform.forward * vertical * _step;
        transform.position += transform.right * horizontal * _step;
    }

}
