using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactables
{
    [Header("Trigger Transforms")]
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;

    [Header("Conditions")]
    [SerializeField] private bool _inEntry;
    [SerializeField] private bool _inExit;

    [SerializeField] private bool _isAscending;
    [SerializeField] private bool _isDescending;

    [Header("VECTOR3 OFFSETS")]
    [SerializeField] private Vector3 _entryOffset;
    [SerializeField] private Vector3 _exitOffset;

    [Header("Player Transform")]
    [SerializeField] private Transform _playerTransform;


    void Start()
    {
        
    }

    void Update()
    {        
        //USER INPUT AND IN TRIGGER
        if(Input.GetKeyDown(KeyCode.E) && _inEntry)
        {
            _isAscending = true;
            _isDescending = false;
        }

        else if(Input.GetKeyDown(KeyCode.E) && _inExit)
        {
            _isAscending = false;
            _isDescending = true;
        }

        //CLIMBING BREAKOUT
        if (_isAscending && _inExit)
        {
            _isAscending = false;
            _playerTransform.position = _exitPoint.position + new Vector3(0, 1, -1);
        }
        else if (_isDescending && _inEntry)
        {
            _isDescending = false;
            _playerTransform.position = _entryPoint.position + new Vector3(0, 0, 1);
        }

        //MOVING ON LADDER
        if (_isAscending)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _exitPoint.position, 1.0f);
        }

        if (_isDescending)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _entryPoint.position, 1.0f);
        }
    }

    public void SetClimbInfo(string ladderPoint)
    {
        if(ladderPoint == "Entry")
        {
            _inEntry = true;
            _inExit = false;
        }

        else if(ladderPoint == "Exit")
        {
            _inEntry = false;
            _inExit = true;
        }
        else if(ladderPoint == "NONE")
        {
            _inEntry = false;
            _inExit = false;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {

    }
}
