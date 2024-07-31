using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Grip_Equip : MonoBehaviour
{
    [Header("Grip Locations")]
    [SerializeField] private Transform _leftGrip;
    [SerializeField] private Transform _rightGrip;
    [SerializeField] private Transform _headGrip;
    [SerializeField] private Transform _neckGrip;
    [SerializeField] private Transform _mustacheGrip;

    [Header("Grip Items")]
    [SerializeField] private GameObject _leftGripItem;
    [SerializeField] private GameObject _rightGripItem;
    [SerializeField] private GameObject _hatOrHair;
    [SerializeField] private GameObject _hairItem;
    [SerializeField] private GameObject _neckGripItem;
    [SerializeField] private GameObject _facialHairItem;

    [Header("Outfit Conditions")]
    [SerializeField] private bool _hasHelmetOrHat;
    [SerializeField] private bool _wearingCape;
    [SerializeField] private bool _hasHair;
    [SerializeField] private bool _hasFacialHair;
    
    [Header("Scalars")]
    [SerializeField] private float _gripScale = 1;

    [SerializeField] private bool _applyLGripScale;
    [SerializeField] private bool _applyRGripScale;
    [SerializeField] private bool _applyHairScale;
    [SerializeField] private bool _applyMustacheScale;
    [SerializeField] private bool _applyNeckScale;


    //BUILT_IN FUNCTIONS
    void Start()
    {
        if(_leftGrip != null && _leftGripItem != null)
            EquipLeftGrip();

        if(_rightGrip != null && _rightGripItem != null)
            EquipRightGrip();

        if (_headGrip != null && _hasHelmetOrHat)
        {
            if (_hatOrHair != null)
                EquipHeadGrip();
        }

        if(_neckGrip != null && _wearingCape)
        {
            if (_neckGripItem != null)
                EquipNeckGrip();
        }
        
        if (_headGrip != null && _hasHair)
        {
            if(_hairItem != null)            
                EquipHair();            
        }

        if(_mustacheGrip != null && _hasFacialHair)
        {
            if (_facialHairItem != null)
                EquipFacialHair();
                            
        }
    
        ApplyGripScale();
    }
    
    //EQUIP GRIP FUNCTIONS
    void EquipLeftGrip()
    {
        var toolInLeftHand = Instantiate(_leftGripItem, _leftGrip.position, _leftGrip.localRotation);
        toolInLeftHand.transform.SetParent(_leftGrip.transform, true);
    }

    void EquipRightGrip()
    {
        var toolInRightHand = Instantiate(_rightGripItem, _rightGrip.position, _rightGrip.localRotation);
        toolInRightHand.transform.SetParent(_rightGrip.transform, true);
    }

    void EquipHeadGrip()
    {
        var hatOnHead = Instantiate(_hatOrHair, _headGrip.position, _headGrip.localRotation);
        hatOnHead.transform.SetParent(_headGrip.transform, true);
    }

    void EquipHair()
    {
        var hairOnHead = Instantiate(_hairItem, _headGrip.position, _headGrip.localRotation);
        hairOnHead.transform.SetParent(_headGrip.transform, true);
    }

    void EquipNeckGrip()
    {
        var capeOnNeck = Instantiate(_neckGripItem, _neckGrip.position, _neckGrip.localRotation);
        capeOnNeck.transform.SetParent(_neckGrip.transform, true);
    }

    void EquipFacialHair()
    {
        var hairOnFace = Instantiate(_facialHairItem, _mustacheGrip.position, _mustacheGrip.localRotation);
        hairOnFace.transform.SetParent(_mustacheGrip.transform, true);
    }

    //SCALE FUNCTIONS
    void ApplyGripScale()
    {
        if(_applyLGripScale)
            _leftGrip.localScale     = new Vector3(_gripScale, _gripScale, _gripScale);
    
        if(_applyRGripScale)
            _rightGrip.localScale    = new Vector3(_gripScale, _gripScale, _gripScale);
    
        if(_applyHairScale)
            _headGrip.localScale     = new Vector3(_gripScale, _gripScale, _gripScale);
    
        if(_applyMustacheScale)        
            _mustacheGrip.localScale = new Vector3(_gripScale, _gripScale, _gripScale);
    
        if(_applyNeckScale)
            _neckGrip.localScale     = new Vector3(_gripScale, _gripScale, _gripScale);
    }
}
