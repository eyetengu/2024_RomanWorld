using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _ladderParent;
    Climbable _climbScript;
    
    [SerializeField] private string _ladderPoints;


    void Start()
    {
        _climbScript = _ladderParent.GetComponent<Climbable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _climbScript.SetClimbInfo(_ladderPoints);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _climbScript.SetClimbInfo("NONE");
        }
    }
}
