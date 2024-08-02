using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayer_V1 : MonoBehaviour
{
    Inventory_Manager       _inventoryUI;

    public List<string>     _acceptedQuests;
    
    public List<GameObject> _gameObjectInventory;
    public List<string>     _objectNameInventory;


    private void Start()
    {
        _inventoryUI = FindObjectOfType<Inventory_Manager>();
    }

    public void AddQuestToQuestList(string questToAdd)
    {
        _acceptedQuests.Add(questToAdd);
    }

    public void RemoveQuestFromList(string questToRemove)
    {
        _acceptedQuests.Remove(questToRemove);

    }

    //PLAYER INVENTORY FUNCTIONS
    public void AddItemToPlayerInventory(GameObject objectToAdd)
    {
        Debug.Log("Player Add Item");

        _gameObjectInventory.Add(objectToAdd);   
        
        string objectName = objectToAdd.name;
        _objectNameInventory.Add(objectName);

        Texture imageToAdd = objectToAdd.GetComponent<QAndDItemPickup_V1>()._image;
        AddItemToInventoryUI(imageToAdd);
    }

    public void RemoveItemFromPlayerInventory(GameObject objectToRemove)
    {
        Debug.Log("Removing Item From PLAYER Inventory");
        _gameObjectInventory.Remove(objectToRemove);        
        
        string objectName = objectToRemove.name;
        _objectNameInventory.Remove(objectName);

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
}
