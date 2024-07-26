using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_Trigger : MonoBehaviour
{
    [SerializeField] private GameObject _skydome;
    [SerializeField] bool _isDay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sun" && _isDay)
        {
            _skydome.SetActive(false);
        }
        
        if (other.tag == "Sun" && _isDay == false)
        {
            _skydome.SetActive(true);
        }
    }
}
