using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform _newWaypoint;
    [SerializeField] private Transform _currentWaypoint;
    [SerializeField] private Transform _previousWaypoint;

    [SerializeField] private float _minDelay, _maxDelay;

    [SerializeField] private List<Transform> _availableWaypoints;

    [SerializeField] float _speed = 3.0f;
    float _step;
    float _turnStep;
    bool _isPaused;
    bool _isHome, _isStall;
    int randomWaypoint;
    [SerializeField] List<Transform> listOfPoints;

    private Animator _animator;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _step = _speed * Time.deltaTime;
        _turnStep = _speed * 50 * Time.deltaTime;
    }

    void Update()
    {
        MoveTowardsWaypoint();
        TurnTowardsWaypoint();
        WaypointDistanceChecker();
    }

    //MOVE & ROTATE FUNCTIONS
    void MoveTowardsWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _newWaypoint.position, _step);
    }

    void TurnTowardsWaypoint()
    {
        Vector3 targetDirection = _newWaypoint.position - transform.position;        

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _turnStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    //CORE FUNCTIONS
    void WaypointDistanceChecker()
    {
        Debug.Log("Checking Distance");

        var distance = Vector3.Distance(transform.position, _newWaypoint.position);

        if (distance < 0.4f && _isPaused == false)
        {
            _isHome = _currentWaypoint.gameObject.GetComponent<WaypointLogic>()._isHome;
            _isStall = _currentWaypoint.gameObject.GetComponent<WaypointLogic>().isStall;

            if (_isHome == true || _isStall == true)
                StartCoroutine(WaypointPauseMin());
            else
                StartCoroutine(WaypointPause());
        }    
    }

    void SelectNextWaypoint()
    {
        listOfPoints = new List<Transform>();
        Debug.Log("Selecting new Waypoint");
        if (_availableWaypoints.Count > 1)
        {
            foreach(var point in _availableWaypoints)
            {
                if(point != _previousWaypoint)
                {
                    _newWaypoint = point;
                    listOfPoints.Add(point);
                }
            }
            int randomPoint = Random.Range(0, listOfPoints.Count);
            _newWaypoint = listOfPoints[randomPoint];
        }
        else
        {
            _newWaypoint = _availableWaypoints[0];
        }
        SetPreviousWaypoint();
        SetCurrentWaypoint();
    }

    public void SetPreviousWaypoint()
    {
        _previousWaypoint = _currentWaypoint;
    }

    public void SetCurrentWaypoint()
    {
        _currentWaypoint = _newWaypoint;
    }

    public void AssignAvailableWaypoints(List<Transform> points)
    {
        Debug.Log("Waypoints Assigned");
        _availableWaypoints = points;
    }

    public void AssignDelayTimes(float min, float max)
    {
        _minDelay = min;
        _maxDelay = max;
    }

    //COROUTINES
    IEnumerator WaypointPause()
    {
        _animator.SetBool("Idle", true);
        _isPaused = true;

        float randomDelay = Random.Range(0, _maxDelay);
        yield return new WaitForSeconds(0.1f);

        _isPaused = false;
        _animator.SetBool("Idle", false);

        SelectNextWaypoint();
    }

    IEnumerator WaypointPauseMin()
    {
        _animator.SetBool("Idle", true);
        _isPaused = true;

        float randomDelay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(randomDelay);

        _isPaused = false;
        _animator.SetBool("Idle", false);

        SelectNextWaypoint();
    }    
}
