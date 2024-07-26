using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower_v3_directional : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    bool _movingForward;
    int waypoint;
    Transform targetWaypoint;
    [SerializeField] float _speed = 2.5f;
    float _step;

    void Start()
    {
        _step = _speed * Time.deltaTime;
        targetWaypoint = waypoints[0];
    }

    void Update()
    {
        MoveToWaypoint();
        TurnTowardsWaypoint();
    }

    void ChooseNextWaypoint()
    {
        targetWaypoint = waypoints[Random.Range(0, waypoints.Count)];
    }

    void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _step);
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < 0.15f)
            ChooseNextWaypoint();
    }

    void TurnTowardsWaypoint()
    {
        Vector3 targetDirection = targetWaypoint.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step * 5, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waypoint" && other.GetComponent<WaypointBehavior>())
        {
            var destinationList = other.GetComponent<WaypointBehavior>();
            waypoints = destinationList.ProvideDestinations();
            ChooseNextWaypoint();
        }
    }
}
