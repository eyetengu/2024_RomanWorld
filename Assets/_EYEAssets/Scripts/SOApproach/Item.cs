using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item.asset", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite icon;
    public int gold;
    public GameObject itemModel;

    public Transform spawnPoint;


    void Start()
    {
        
    }


    public void PrintName()
    {
        Debug.Log("Item: " + itemName);
    }

    public void InstantiateObject()
    {
        var model = Instantiate(itemModel);
        model.transform.SetParent(spawnPoint);
    }
}
