using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public InputAction action;

    public CinemachineVirtualCamera villageCam;
    public CinemachineVirtualCamera towerCam;

    private Animator animator;
    private bool villageCamera = true;

    private void Awake()
    {
        //animator = this.GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => SwitchPriority();
    }

    private void SwitchState()
    {
        if(villageCamera)
        {
            animator.Play("TowerCamera");
        }
        else
        {
            animator.Play("VillageCamera");
        }
        villageCamera = !villageCamera;
    }
    
    private void SwitchPriority()
    {
        if (villageCamera)
        {
            villageCam.Priority = 1;
            towerCam.Priority = 0;
        }
        else
        {
            villageCam.Priority = 0;
            towerCam.Priority = 1;
        }
        villageCamera = !villageCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
