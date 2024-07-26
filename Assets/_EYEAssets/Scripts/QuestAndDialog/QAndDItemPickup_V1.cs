using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAndDItemPickup_V1 : MonoBehaviour
{
    [SerializeField] string _objectName;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    private void Start()
    {
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
            Destroy(gameObject, 1);
        }
    }
}
