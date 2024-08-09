using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    Game_Manager _gameManager;
    [Header("INVENTORY")]
    [SerializeField] private GameObject _inventoryPanel;

    [Header("LISTS")]
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private RawImage[] _itemSlots;
    [SerializeField] bool[] _itemPresent;

    //[SerializeField] private Texture _image;
    [Header("SPRITE")]
    [SerializeField] private Texture _sprite;

    bool _showInventory;
    bool _inventoryIsFull;

    int _itemID;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        _gameManager = FindObjectOfType<Game_Manager>();

        _inventoryPanel.SetActive(false);

        foreach(var item in _itemSlots)
        {
            //item.texture = _sprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventoryPanel();        
        }
    }


    //CORE FUNCTIONS
    void ShowInventoryPanel()
    {
        _showInventory = !_showInventory;

        if (_showInventory)
        {
            _inventoryPanel.SetActive(true);
            _gameManager.ShowAndConfineCursor();
            _gameManager.PausePlayer();
        }
        else
        {
            _gameManager.LockCursorInvisible();
            _inventoryPanel.SetActive(false);
            _gameManager.UnpausePlayer();
        }
    }

    public void AddItemToInventory(Texture newImage)
    {
        _itemID = 0;

        foreach (var item in _itemPresent)
        {
            
            if(item == false)
            {
                _itemSlots[_itemID].texture = newImage;
                _itemPresent[_itemID] = true;
                break;
            }

            _itemID++;
            if (_itemID > _itemSlots.Length - 1)
            {
                Debug.Log("Inventory Is Full");
                _inventoryIsFull = true;
            }
            else
                _inventoryIsFull = false;
        }
    }

    public void RemoveItemFromInventory(Texture imageToRemove)
    {
        _itemID = 0;

        foreach (var item in _itemPresent)
        {
            _itemID++;
                
            if (item == true)
            {                    
                if (_itemSlots[_itemID].texture == imageToRemove)
                {
                    _itemSlots[_itemID].texture = null;
                    _itemPresent[_itemID] = false;
                    break;
                }
            }
        }        
    }
}
