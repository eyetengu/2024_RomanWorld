using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDialog : MonoBehaviour
{
    QuestPlayer_V1 _playerInventory;
    [SerializeField] Quest_Manager _questManager;
    [SerializeField] UI_Manager _UIManager;

    SkinnedMeshRenderer _vendorRenderer;
    [SerializeField] Material _completedQuestColor;

    //QUEST DATA
    [SerializeField] private string _questName;
    [SerializeField] private string _rewardName;    
    [SerializeField] private List<string> _questObjectives;    
    [SerializeField] private GameObject _rewardObject;
    string _message;

    int itemsCollected;

    public bool _inZone;
    public bool _questExplained;
    public bool _questAccepted;
    public bool _questComplete;
    public bool _finalDialogFinished;

    //DIALOG DATA
    [SerializeField] private string _introDialog;
    [SerializeField] private string _informationDialog;
    [SerializeField] private string _acceptDialog;
    [SerializeField] private string _questCompleteDialog;
    [SerializeField] private GameObject _questIcon;


    private void Start()
    {
        _vendorRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _UIManager = GameObject.FindObjectOfType<UI_Manager>().GetComponent<UI_Manager>();
    }

    void Update()
    {
        if (_inZone)
        {
            if(_questComplete == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(_questAccepted == false)
                    {
                        if (_questExplained == false)
                        {
                            string requestItems = ". ";
                            foreach(var item in _questObjectives)
                            {
                                requestItems += item.ToString() + ", ";
                            }

                            _message = _informationDialog + requestItems;
                            _UIManager.DisplayPlayerMessage(_message);
                            Debug.Log(_informationDialog + requestItems);

                            _questExplained = true;
                        }
                        else if (_questExplained)
                        {
                            _questExplained = true;
                            _questAccepted = true;
                            _questManager.AddActiveQuest(_questName);
                            _playerInventory._acceptedQuests.Add(_questName);

                            _message = _acceptDialog;
                            _UIManager.DisplayPlayerMessage(_message);
                            Debug.Log(_acceptDialog);
                        }
                    }
                    else if (_questAccepted)
                    {
                        itemsCollected = 0;
                        foreach(var item in _questObjectives)
                        {
                            foreach(var item2 in _playerInventory._playerInventory)
                            {
                                if(item == item2)
                                {
                                    itemsCollected++;
                                    _playerInventory._playerInventory.Remove(item2);
                                    
                                    if (itemsCollected == _questObjectives.Count)
                                    {
                                        _questComplete = true;

                                        _questManager.AddCompletedQuest(_questName);

                                        _playerInventory._acceptedQuests.Remove(_questName);

                                        _message = _questCompleteDialog + " " + _rewardName + " I promised";
                                        _UIManager.DisplayPlayerMessage(_message);
                                        Debug.Log(_questCompleteDialog);

                                        _finalDialogFinished = true;
                                        _playerInventory._playerInventory.Add(_rewardName);
                                        //_vendorRenderer.material = _completedQuestColor;
                                        _questIcon.SetActive(false);
                                        return;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                } 
            }            
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _playerInventory = other.GetComponent<QuestPlayer_V1>();
            _inZone = true;
            if(_questAccepted == false)
            {
                _message = _introDialog;
                _UIManager.DisplayPlayerMessage(_message);
                Debug.Log(_introDialog);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _inZone = false;
    }
}



