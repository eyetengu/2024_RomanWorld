using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivated : MonoBehaviour
{
    [SerializeField] private GameObject _weaponFX;
    [SerializeField] private string _activatingWeapon;
    private UI_MusicManager _musicManager;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Transform _fxPoint;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        _musicManager = FindObjectOfType<UI_MusicManager>();
    }


    //TRIGGER FUNCTIONS
    private void OnTriggerEnter(Collider other)
    {
        var weapon = other.GetComponent<Weapon> ();
        if(weapon != null && weapon.WeaponName == _activatingWeapon)
        {
            _musicManager.PlayGeneralAudioClip(_audioClip);
            Instantiate(_weaponFX, _fxPoint.transform);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //_weaponFX.SetActive(true);
            Debug.Log("Contacted by: " + other.name);
            Destroy(gameObject, 1);
        }
    }
}
