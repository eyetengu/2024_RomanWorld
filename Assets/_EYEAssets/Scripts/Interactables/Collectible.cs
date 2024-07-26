using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactables
{
    enum valueTypes { health, coin, experience, score };
        [SerializeField] valueTypes _valueTypes;

    [SerializeField] private int _collectibleValue;
    string _uiMessage;
    [SerializeField] private UI_Manager _uiManager;

    //CORE FUNCTIONS
    public override void RunParticleEffect()
    {
        base.RunParticleEffect();
    }

    public override void PlayAudioClip()
    {
        base.PlayAudioClip();
    }


    //TRIGGER FUNCTIONS
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.tag == "Player")
        {
            _uiMessage = "Collecting " + _valueTypes + " Collectible";
            _uiManager.UpdateUIMessage(_uiMessage);
            PlayAudioClip();

            if (_valueTypes == valueTypes.experience)
            { 
                Debug.Log(_collectibleValue + " Experience Added");
                _uiManager.UpdateExperience(_collectibleValue);
            }      
            else if (_valueTypes == valueTypes.coin)
            { 
                Debug.Log(_collectibleValue + " Coin Added"); 
                _uiManager.UpdateGold(_collectibleValue);
            }
            else if (_valueTypes == valueTypes.health)
            { 
                Debug.Log(_collectibleValue + " Health Added"); 
                _uiManager.UpdateHealth(_collectibleValue);
            }
            else if(_valueTypes == valueTypes.score) 
            { 
                Debug.Log(_collectibleValue + " Score Added");
                _uiManager.UpdateScore(_collectibleValue);
            }

            Destroy(gameObject, 1.2f);
        }
    }
}
