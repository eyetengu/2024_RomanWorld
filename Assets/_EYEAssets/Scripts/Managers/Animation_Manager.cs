using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetIdle();
    }

    private void Update()
    {
        
    }

    public void SetIdle()
    {
        if (_animator != null)
        {
            if (_animator.GetBool("Running") == true)
                _animator.SetBool("Running", false);


            _animator.SetBool("Idle", true);
        }
    }

    public void SetWalk(bool idle, bool backwards)
    {
        //if (_animator.GetBool("Running") == true)
            //_animator.SetBool("Running", false);

        _animator.SetBool("Idle", idle);
        _animator.SetBool("Backwards", backwards);
    }

    public void SetRun(bool running, bool backwards)
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("Running", true);
    }

    public void SetBackwardsBool(bool value)
    {
        _animator.SetBool("Backwards", value);
    }

    public void StrafeLeft(bool value)
    {
        _animator.SetBool("StrafeLeft", value);
    }

    public void StrafeRight(bool value)
    {
        _animator.SetBool("StrafeRight", value);
    }

}
