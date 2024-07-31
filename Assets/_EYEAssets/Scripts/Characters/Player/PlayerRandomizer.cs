using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomizer : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;

    [SerializeField] private Material[] outfits;
    
    private int materialCount;
    private int randomOutfit;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialCount = outfits.Length - 1;
        ChangeOutfit();
    }

    void Update()
    {
        //Following lines useful for debugging purposes
        //if (Input.GetKey(KeyCode.Alpha1))
            //ChangeOutfit();
    }

    //CORE FUNCTIONS
    void ChangeOutfit()
    {
        randomOutfit = Random.Range(0, materialCount);
        _renderer.material = outfits[randomOutfit];
    }
}
