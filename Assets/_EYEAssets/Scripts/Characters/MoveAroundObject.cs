using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private float _step;

    void Start()
    {
    }


    void FixedUpdate()
    {
        _step = _speed * Time.deltaTime;
        transform.Rotate(0,_step, 0);
    }
}
