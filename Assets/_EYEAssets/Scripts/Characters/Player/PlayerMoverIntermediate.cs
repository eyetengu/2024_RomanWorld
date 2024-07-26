using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverIntermediate : MonoBehaviour
{
    float _speed = 3.0f;
    float _step;
    float _speedMultiplier = 1.0f;
 

    void Start()
    {
        
    }

    void Update()
    {
        _step = _speed * _speedMultiplier;

        PlayerMover();
    }

   void PlayerMover()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(0, 0, vertical);
        transform.Rotate(0, horizontal * _step *10, 0);

        transform.TransformDirection(transform.forward);
        transform.position += transform.forward * vertical * _step;
    }
}
