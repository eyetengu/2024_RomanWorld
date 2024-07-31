using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDialog : MonoBehaviour
{
    Quest_Manager _questManager;
    [SerializeField] UI_Manager _UIManager;
    QuestPlayer_V1 _playerInventory;

    Animator _animator;

    //QUEST DATA
    [Header("Quest Data")]
    [SerializeField] private string _questName;
    [SerializeField] private string _rewardName;    
    [SerializeField] private List<string> _questObjectives;    
    [SerializeField] private GameObject _rewardObject;
    [SerializeField] private GameObject _questIcon;

    //DIALOG DATA
    [Header("Dialog Elements")]
    [SerializeField] private string _introDialog;
    [SerializeField] private string _informationDialog;
    [SerializeField] private string _acceptDialog;
    [SerializeField] private string _questCompleteDialog;

    SkinnedMeshRenderer _vendorRenderer;

    string _message;
    int itemsCollected;

    public bool _inZone;
    public bool _questExplained;
    public bool _questAccepted;
    public bool _questComplete;
    public bool _finalDialogFinished;

    float _rotationSpeed = 2.0f;
    float _rotationStep;

    [SerializeField] private Material[] _questIconColors;
    private MeshRenderer _questIconRenderer;


    //BUILT-IN FUNCTIONS
    private void Start()
    {
        _questManager = FindObjectOfType<Quest_Manager>();        
        _UIManager = GameObject.FindObjectOfType<UI_Manager>().GetComponent<UI_Manager>();
        _vendorRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _questIconRenderer = _questIcon.GetComponent<MeshRenderer>();
        _questIconRenderer.material = _questIconColors[0];
    }

    void Update()
    {
        _rotationStep = _rotationSpeed * Time.deltaTime;

        if (_inZone)
        {
            TurnToFacePlayer();
            if (_questComplete == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(_questAccepted == false)
                    {
                        if (_questExplained == false)
                        {
                            string requestItems = " ";
                            foreach (var item in _questObjectives)
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
                            _questIconRenderer.material = _questIconColors[1];
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

                                        _animator.SetBool("Item Received", true);

                                        _questManager.AddCompletedQuest(_questName);

                                        _playerInventory._acceptedQuests.Remove(_questName);

                                        _message = _questCompleteDialog + " " + _rewardName + " I promised";
                                        _UIManager.DisplayPlayerMessage(_message);
                                        Debug.Log(_questCompleteDialog);

                                        _finalDialogFinished = true;
                                        _playerInventory._playerInventory.Add(_rewardName);
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

    //CORE FUNCTIONS
    void TurnToFacePlayer()
    {
        Vector3 targetDirection = _playerInventory.gameObject.transform.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    //TRIGGER FUNCTIONS
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
