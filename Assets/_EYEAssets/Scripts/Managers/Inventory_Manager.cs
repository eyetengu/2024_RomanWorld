using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] private RawImage[] _itemSlots;
    [SerializeField] bool[] _itemPresent;
    
    [SerializeField] private Texture _image;
    [SerializeField] private Texture _sprite;

    bool _showInventory;
    int _itemID;

    [SerializeField] private GameObject _inventoryPanel;


    void Start()
    {
        _inventoryPanel.SetActive(false);

        foreach(var item in _itemSlots)
        {
            //item.texture = _sprite;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ShowInventoryPanel();        
    }

    void ShowInventoryPanel()
    {
        _showInventory = !_showInventory;

        if (_showInventory)            
            _inventoryPanel.SetActive(true);
            
        else
            _inventoryPanel.SetActive(false);

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
        }
    }

}
