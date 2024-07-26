using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int waypointMax;
    private Transform targetPoint;
    private int randomWaypoint;
    float _step = 3;
    float _speed = 2.5f;


    void Start()
    {
        waypointMax = waypoints.Length;
        ChooseRandomWaypoint();
    }

    void Update()
    {
        _step = _speed * Time.deltaTime;    
        MoveToWaypoint();
        TurnToFaceTargetPoint();
    }

    void ChooseRandomWaypoint()
    {
        randomWaypoint = Random.Range(0, waypointMax);
        targetPoint = waypoints[randomWaypoint];
    }

    void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _step);
        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance < 0.3f)
            ChooseRandomWaypoint();
    }

    void TurnToFaceTargetPoint()
    {
        Vector3 targetDirection = targetPoint.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
