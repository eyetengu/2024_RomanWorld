using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Follower : MonoBehaviour
{
    [SerializeField] private Transform _commander;

    float _speed = 3.0f;
    float _step;


    void Start()
    {
        
    }

    void Update()
    {
        _step = _speed * Time.deltaTime;

        FollowCommander();
        FaceCommander();
    }

    private void FollowCommander()
    {
        float distance = Vector3.Distance(transform.position, _commander.position);
        if (distance > 1.5f)
            transform.position = Vector3.MoveTowards(transform.position, _commander.position, _step);            
    }

    private void FaceCommander()
    {
        Vector3 targetDirection = _commander.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
