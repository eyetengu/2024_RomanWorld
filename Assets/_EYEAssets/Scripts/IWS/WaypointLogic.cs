using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointLogic : MonoBehaviour
{
    WaypointFollower _npcFollower;
    [SerializeField] private List<Transform> _accessableWaypoints;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    public bool _isHome, isStall;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Follower")
        {
            //Debug.Log("Follower Receiving");
            _npcFollower = other.GetComponent<WaypointFollower>();
            if(_npcFollower != null)
            {
                _npcFollower.AssignAvailableWaypoints(_accessableWaypoints);
                _npcFollower.AssignDelayTimes(_minDelay, _maxDelay);
            }
        }
    }   
}
