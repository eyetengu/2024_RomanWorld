using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayer_V1 : MonoBehaviour
{
    Inventory_Manager _inventoryUI;
    UI_Manager _uiManager;
    [Header("QUESTS")]
    public List<string>     _acceptedQuests;

    [Header("INVENTORIES")]
    public List<GameObject> _gameObjectInventory;
    public List<string>     _objectNameInventory;
    List<bool> _equipableTerms;

    [SerializeField] List<GameObject> _equipables;
    [SerializeField] GameObject _gripNullObject;
    int _equipedItemID;
    [SerializeField] Transform _gripR;
    [SerializeField] Transform _gripL;
    GameObject _equipedItem;


    //BUILT-IN FUNCTIONS
    private void Start()
    {
        _uiManager = FindObjectOfType<UI_Manager>();
        _equipables = new List<GameObject>();
        _equipableTerms = new List<bool>();
        _inventoryUI = FindObjectOfType<Inventory_Manager>();
        EquipNullObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {            
            EquipItem();
        }
    }

    //QUEST FUNCTIONS
    public void AddQuestToQuestList(string questToAdd)
    {
        _acceptedQuests.Add(questToAdd);
    }

    public void RemoveQuestFromList(string questToRemove)
    {
        _acceptedQuests.Remove(questToRemove);

    }


    //PLAYER INVENTORY FUNCTIONS
    void EquipNullObject()
    {
        var nullObject = Instantiate(_gripNullObject, _gripR);
        _equipables.Add(nullObject);
        nullObject.transform.SetParent(_gripR);
        
    }

    public void AddItemToPlayerInventory(GameObject objectToAdd)
    {
        Debug.Log("Player Add Item");

        if (objectToAdd.GetComponent<QAndDItemPickup_V1>().CheckIfItemIsEquipable())
        {
            _equipables.Add(objectToAdd);
            objectToAdd.transform.SetParent(_gripR, false);
            objectToAdd.transform.position = _gripR.position;
            objectToAdd.transform.rotation = _gripR.localRotation;
            objectToAdd.SetActive(false);
        }

        _gameObjectInventory.Add(objectToAdd);   
        
        string objectName = objectToAdd.name;
        _objectNameInventory.Add(objectName);

        bool equipable = objectToAdd.GetComponent<QAndDItemPickup_V1>().CheckIfItemIsEquipable();
        _equipableTerms.Add(equipable);

        //CreateEquipableList();

        Texture imageToAdd = objectToAdd.GetComponent<QAndDItemPickup_V1>()._image;
        AddItemToInventoryUI(imageToAdd);
    }

    public void RemoveItemFromPlayerInventory(GameObject objectToRemove)
    {
        Debug.Log("Removing Item From PLAYER Inventory");
        _gameObjectInventory.Remove(objectToRemove);        
        
        string objectName = objectToRemove.name;
        _objectNameInventory.Remove(objectName);

        bool equipable = objectToRemove.GetComponent<QAndDItemPickup_V1>().CheckIfItemIsEquipable();
        _equipableTerms.Remove(equipable);

        //CreateEquipableList();

        Texture imageToRemove = objectToRemove.GetComponent<QAndDItemPickup_V1>()._image;
        if (imageToRemove == null)
            Debug.Log("Image To Remove Is Missing");
        RemoveItemFromInventoryUI(imageToRemove);
    }


    //INVENTORY UI FUNCTIONS
    public void AddItemToInventoryUI(Texture addTexture)
    {
        if(_inventoryUI != null)
        { 
            Debug.Log("Adding texture to ui");
            
            if(addTexture != null)
                _inventoryUI.AddItemToInventory(addTexture);
        }
    }

    public void RemoveItemFromInventoryUI(Texture removeTexture)
    {
        Debug.Log("Removing UI Texture");
        if(_inventoryUI != null)
        {
            if(removeTexture != null)
                _inventoryUI.RemoveItemFromInventory(removeTexture);
        }
    }


    //EQUIP OBJECT FUNCTIONS
    public void CreateEquipableList()
    {
        /*
        int objectID = 0;
        _equipables = new List<GameObject>();
        //_equipables.Add(null);

        foreach(bool item in _equipableTerms)
        {
            if (item)
            {
                _equipables.Add(_gameObjectInventory[objectID]);
                _gameObjectInventory[objectID].transform.SetParent(_gripR, false);
            }
    
            objectID++;
        }
        */
    }

    public void EquipItem()
    {
        _equipedItemID++;
        if (_equipedItemID >= _equipables.Count)
            _equipedItemID = 0;

        foreach (var item in _equipables)
            item.SetActive(false);

        _equipables[_equipedItemID].SetActive(true);

        var equippedItem = _equipables[_equipedItemID].GetComponent<QAndDItemPickup_V1>()._image;
        _uiManager.ShowEquippedItem(equippedItem);

        Debug.Log("Equipped Item: " + _equipables[_equipedItemID] + " " + _equipedItemID.ToString());
    }
}
