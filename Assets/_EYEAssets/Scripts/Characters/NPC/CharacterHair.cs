using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHair : MonoBehaviour
{
    [SerializeField] MeshRenderer _hair;
    [SerializeField] MeshRenderer _beard;
    [SerializeField] Material[] _hairColor;

    int _hairColorIndex;
    int _beardColorIndex;

    [SerializeField] bool _randomHair;
    [SerializeField] bool _isHairAndBeardTheSame;

    //BUILT-IN FUNCTIONS
    void Start()
    {
        BoolChecksAndHairChange();
//        ChangeHairAndBeard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Uncomment following line for debugging purposes
            //BoolChecksAndHairChange();
        }
    }

    void BoolChecksAndHairChange()
    {
        if (_randomHair && _isHairAndBeardTheSame)
        {
            _hairColorIndex = Random.Range(0, _hairColor.Length - 1);

            ChangeHairAndBeard();
        }
        else if (_randomHair && _isHairAndBeardTheSame == false)
        {
            _hairColorIndex = Random.Range(0, _hairColor.Length - 1);

            MixHairAndBeard();
        }
        else if (_randomHair == false && _isHairAndBeardTheSame)
        {
            _hairColorIndex++;
            if (_hairColorIndex > _hairColor.Length - 1)
                _hairColorIndex = 0;

            ChangeHairAndBeard();
        }
        else if (_randomHair == false && _isHairAndBeardTheSame == false)
        {
            _hairColorIndex++;
            if (_hairColorIndex > _hairColor.Length - 1)
                _hairColorIndex = 0;

            MixHairAndBeard();
        }
    }

    //CORE FUNCTIONS
    void ChangeHairAndBeard()   
    {
        if(_hair != null)
            _hair.material = _hairColor[_hairColorIndex];
        if(_beard != null)
            _beard.material = _hairColor[_hairColorIndex];
    }

    void MixHairAndBeard()
    {
        if(_hair != null)
            _hair.material = _hairColor[_hairColorIndex];
        
        if(_beard != null)
            _beardColorIndex = Random.Range(0, _hairColor.Length - 1);
        
        if(_beardColorIndex == _hairColorIndex)
            StartCoroutine(AttemptBeardColor());

        if(_beard != null)
            _beard.material = _hairColor[_beardColorIndex];
    }

    //COROUTINES
    IEnumerator AttemptBeardColor()
    {
        yield return new WaitForSeconds(0.01f);
        MixHairAndBeard();
    }
}
