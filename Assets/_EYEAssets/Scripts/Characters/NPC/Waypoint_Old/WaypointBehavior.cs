using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{
    [SerializeField] private List<Transform> _altDestinations;


    public List<Transform> ProvideDestinations()
    {
        return _altDestinations;
    }
}
