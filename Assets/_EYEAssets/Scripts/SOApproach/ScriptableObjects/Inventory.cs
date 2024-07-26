using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryItems;


    void Start()
    {
        inventoryItems.ForEach(i => i.PrintName());
        inventoryItems.ForEach(i => i.InstantiateObject());
    }

    void Update()
    {
        
    }
}
