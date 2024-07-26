using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomizer : MonoBehaviour
{
    [SerializeField] private Material[] outfits;
    private int materialCount;
    private int randomOutfit;
    private SkinnedMeshRenderer _renderer;

    void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialCount = outfits.Length - 1;
        ChangeOutfit();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            ChangeOutfit();
    }

    void ChangeOutfit()
    {
        randomOutfit = Random.Range(0, materialCount);
        _renderer.material = outfits[randomOutfit];
    }
}
