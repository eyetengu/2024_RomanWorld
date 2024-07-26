using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAndTurn : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] bool _rotateObject;
    [SerializeField] float _rotationSpeed;
    [SerializeField] private bool _xAxisRot, _yAxisRot, _zAxisRot;

    [Header("Movement")]
    [SerializeField] bool _moveObject;
    [SerializeField] float _moveSpeed;
    [SerializeField] private bool _xAxisMove, _yAxisMove, _zAxisMove;
    //[SerializeField] private float _moveX, _moveY, _moveZ;
    bool _moving;

    float _step;
    float _moveStep;
    [Header("Bounce Delay")]
    [SerializeField] float _bounceDelay;


    void Start()
    {
        
    }

    void Update()
    {
        _step = _rotationSpeed * Time.deltaTime;
        _moveStep = _moveSpeed * Time.deltaTime;

        if (_rotateObject)
        {
            RotateObject();
        }
        if (_moveObject)
        {
            MoveObject();
        }
    }

    void RotateObject()
    {
        if(_xAxisRot) { transform.Rotate( _step, 0, 0); }
        if(_yAxisRot) { transform.Rotate( 0, _step, 0); }
        if(_zAxisRot) { transform.Rotate( 0, 0, _step); }
    }

    void MoveObject()
    {
        if (_xAxisMove) { transform.position += new Vector3(_moveStep, 0, 0 ); }
        if (_yAxisMove) { transform.position += new Vector3(0, _moveStep, 0); }
        if (_zAxisMove) { transform.position += new Vector3(0, 0, _moveStep); }

        if (_moving == false)
            StartCoroutine(ObjectBounce());
        //transform.position += new Vector3(_moveX, _moveY, _moveZ) * _moveStep;
    }

    IEnumerator ObjectBounce()
    {
        _moving = true;
        yield return new WaitForSeconds(_bounceDelay);
        _moving = false;
        _moveSpeed = _moveSpeed * -1;
    }
}
