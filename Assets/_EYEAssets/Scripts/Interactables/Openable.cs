using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : Interactables
{
    enum OpenableType { SlideUp, SlideSide, Revolving, DoorSingle, DoorDouble, CrateLid }
    [SerializeField] OpenableType _openableType;

    [SerializeField] private bool _isLocked, _isOpened;
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private Transform _doorPivot2;

    string _uiMessage;
    [SerializeField] private UI_Manager _uiManager;
    [SerializeField] bool _isDoubleSlide;


    //CORE FUNCTIONS
    void FiniteStateMachine()
    {
        switch (_openableType)
        {
            case OpenableType.SlideUp:
                break;
            case OpenableType.SlideSide:
                if (_isDoubleSlide)
                {
                    if (_isOpened)
                    {
                        _doorPivot.position = _doorPivot.position + new Vector3(0.5f, 0, 0);
                        _doorPivot2.position = _doorPivot2.position + new Vector3(-0.5f, 0, 0);
                    }
                    if (_isOpened == false)
                    {
                        _doorPivot.position = _doorPivot.position + new Vector3(-0.5f, 0, 0);
                        _doorPivot2.position = _doorPivot2.position + new Vector3(0.5f, 0, 0);
                    }
                }
                else 
                { 
                    if (_isOpened)
                        _doorPivot.position = _doorPivot.position + new Vector3(1, 0, 0);
                    if (_isOpened == false)
                        _doorPivot.position = _doorPivot.position + new Vector3(-1, 0, 0);
                }

                break;
            case OpenableType.DoorSingle:
                if (_isOpened)                
                    _doorPivot.Rotate(0, 90, 0);                                    
                else if (_isOpened == false)                
                    _doorPivot.Rotate(0, -90, 0);                
                break;
            case OpenableType.DoorDouble:
                if (_isOpened)
                {
                    _doorPivot.Rotate(0, 90, 0);
                    _doorPivot2.Rotate(0, -90, 0);
                }
                if (_isOpened == false)
                { 
                    _doorPivot.Rotate(0, -90, 0);
                    _doorPivot2.Rotate(0, 90, 0);
                }
                break;
            case OpenableType.Revolving:
                break;
            case OpenableType.CrateLid:
                break;
            default:
                break;
        }
    }

    public override void UpdateUIManagerMessage(string message)
    {        
        _uiManager.UpdateUIMessage(_uiMessage);
    }

    //TRIGGER FUNCTIONS
    public override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && _isOpened == false)
        {
            _isOpened = true;

            _uiMessage = "Opening " + _openableType.ToString() + " Door";
            _uiManager.UpdateUIMessage(_uiMessage);
            Debug.Log("Opening Door");
            
            FiniteStateMachine();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && _isOpened)
        {
            _isOpened = false;

            _uiMessage = "Closing " + _openableType.ToString() + " Door";
            _uiManager.UpdateUIMessage(_uiMessage);
            Debug.Log("Closing Door");
            
            FiniteStateMachine();
        }
    }
}
