using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : Interactables
{
    [SerializeField] private float _blastRadius;
    [SerializeField] private float _blastDelay;
    

    //CORE FUNCTIONS

    public override void RunParticleEffect()
    {
        base.RunParticleEffect();
    }

    //TRIGGER FUNCTIONS
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);        
    }
}
