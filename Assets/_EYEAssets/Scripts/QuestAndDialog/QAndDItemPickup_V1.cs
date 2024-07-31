using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class QAndDItemPickup_V1 : MonoBehaviour
{
    [SerializeField] UI_Manager _uiManager;
    [SerializeField] Inventory_Manager _inventoryManager;

    AudioSource _audioSource;

    [SerializeField] string _objectName;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] private Texture _image;


    private void Start()
    {
        _uiManager = GameObject.FindObjectOfType<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _audioSource.PlayOneShot(_audioClip);   

            var collider = gameObject.GetComponent<SphereCollider>();
            collider.enabled = false;

            var playerInventory = other.GetComponent<QuestPlayer_V1>();
            playerInventory._playerInventory.Add(_objectName);

            if (_image != null)
            {
                _uiManager.ShowEquippedItem(_image);
                _inventoryManager.AddItemToInventory(_image);
            }

            Destroy(gameObject, 1);
        }
    }
}
