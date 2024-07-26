using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindingWheelBehavior : MonoBehaviour
{
    //[SerializeField] private Transform _grindingWheel;
    [SerializeField] private float _speed = 50;
    private float _step;


    void Start()
    {
        
    }

    void Update()
    {
        _step = _speed * Time.deltaTime;

        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }
}
