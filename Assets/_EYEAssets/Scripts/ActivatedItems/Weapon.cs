using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string _weaponName;

    public string WeaponName { get => _weaponName; }
}
