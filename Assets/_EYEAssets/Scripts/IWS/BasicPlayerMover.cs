using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMover : MonoBehaviour
{
    float _speed = 3.0f;
    float _speedMultiplier = 1.0f;
    float _step;


    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;
        PlayerMover();
        UserInput();
    }

    private void FixedUpdate()
    {
        //PlayerMover();
    }
    void PlayerMover()
    {
        //setting up the user input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //assigning the user inputs
        Vector3 direction = new Vector3(0, 0, vertical);

        //player rotation
        transform.Rotate(0, horizontal * _step * 10, 0);
                    //transform.TransformDirection(transform.forward);          unnecessary code line
        
        //player movement
        if(vertical > 0)
            transform.position += transform.forward * _step;
        else if (vertical < 0)
            transform.position += transform.forward * -_step;
    }

    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _speedMultiplier = 3.0f;
    }
}
