using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivated : MonoBehaviour
{
    [SerializeField] private GameObject _weaponFX;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Weapon>() != null)
        {
            Instantiate(_weaponFX, transform);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //_weaponFX.SetActive(true);
            Debug.Log("Contacted by: " + other.name);
            Destroy(gameObject, 1);
        }
    }
}
