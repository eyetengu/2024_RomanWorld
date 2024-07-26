using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonOrbit : MonoBehaviour
{
    [SerializeField] private float _speed = 3000.0f;
    private float _step;

    void Start()
    {
        _step = _speed * Time.deltaTime;
    }


    void FixedUpdate()
    {
        transform.Rotate(_step,0, 0);
    }
}
