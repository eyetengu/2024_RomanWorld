using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class QAndDItemPickup_V1 : MonoBehaviour
{    
    [SerializeField] UI_Manager _uiManager;
    UI_MusicManager _musicUIManager;
    [SerializeField] Inventory_Manager _inventoryManager;

    AudioSource _audioSource;

    public string _objectName;
    [SerializeField] AudioClip _audioClip;
    public Texture _image;


    //BUILT-IN FUNCTIONS
    private void Start()
    {
        _musicUIManager = FindObjectOfType<UI_MusicManager>();
        _uiManager = GameObject.FindObjectOfType<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
    }

    //TRIGGER FUNCTIONS
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _musicUIManager.PlayGeneralAudioClip(_audioClip);

            var collider = gameObject.GetComponent<SphereCollider>();
            collider.enabled = false;

            var playerInventory = other.GetComponent<QuestPlayer_V1>();
            //playerInventory._inventoryObject.Add(this.gameObject);

            if (_image != null)
            {
                _uiManager.ShowEquippedItem(_image);
                playerInventory.AddItemToPlayerInventory(this.gameObject);
            }

            this.gameObject.SetActive(false);
        }
    }
}
