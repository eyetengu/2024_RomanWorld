using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDialog : MonoBehaviour
{
    Quest_Manager _questManager;
    [SerializeField] UI_Manager _UIManager;
    QuestPlayer_V1 _playerInventory;
    Inventory_Manager _inventoryManager;

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
        _inventoryManager = FindObjectOfType<Inventory_Manager>();
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
                            _questExplained = true;
                            
                            string requestItems = " ";
                            foreach (var item in _questObjectives)                            
                                requestItems += item.ToString() + ", ";                            

                            _message = _informationDialog + requestItems;
                            
                            _UIManager.DisplayPlayerMessage(_message);
                            Debug.Log(_informationDialog + requestItems);
                        }
                        else if (_questExplained)
                        {
                            _message = _acceptDialog;
                            Debug.Log(_acceptDialog);
                            
                            _questExplained = true;

                            _questAccepted = true;
                            _playerInventory.AddQuestToQuestList(_questName);
                            _questManager.AddActiveQuest(_questName);
                            
                            _UIManager.DisplayPlayerMessage(_message);
                            
                            _questIconRenderer.material = _questIconColors[1];                            
                        }
                    }
                    else if (_questAccepted)                                                            //When the player accepts the vendor's quest
                    {
                        itemsCollected = 0;                                                             //The number of quest objective items collected is reset to zero
                        foreach(var item in _questObjectives)                                           //The vendor's quest objective is obtained...
                        {
                            List<GameObject> gameObjectInventory = _playerInventory._gameObjectInventory;   //A list of the player's inventory items is collected
                                
                            foreach(GameObject item2 in gameObjectInventory)                            //The player's inventory list is compared to the vendor's list
                            {
                                if (item == item2.GetComponent<QAndDItemPickup_V1>()._objectName)       //if there is a match...
                                {
                                    itemsCollected++;                                                   //The number of collected items is increased
                                    _playerInventory.RemoveItemFromPlayerInventory(item2);              //The item that matches is removed from the player's inventory

                                    if (itemsCollected == _questObjectives.Count)                       //If all vendor items are collected...
                                    {
                                        _questComplete = true;                                          //This quest is marked complete
                                        _finalDialogFinished = true;                                    //The final dialog is marked as finished

                                        _animator.SetBool("Item Received", true);                       //The vendor's 'quest finished' animation is triggered

                                        _questManager.AddCompletedQuest(_questName);                    //The quest is added(completed quest list) & removed(active quest list) (Quest MGR)
                                        
                                        _playerInventory.RemoveQuestFromList(_questName);               //The quest is removed from the player's list of active quests
                                        _playerInventory.AddItemToPlayerInventory(_rewardObject);       //Reward item added to player's inventory

                                        _message = _questCompleteDialog + " " + _rewardName + " I promised";    //Message created
                                        _UIManager.DisplayPlayerMessage(_message);                              //Message sent to UI_Manager to be displayed in player message area
                                        //Debug.Log(_questCompleteDialog);

                                        _questIcon.SetActive(false);                                    //Quest Icon is removed from above player's head

                                        return;                                                         //exit _questAccepted condition
                                    }
                                    break;                                                              //if items match- continue foreach(GO item2 in gameObjectInventory) loop
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
