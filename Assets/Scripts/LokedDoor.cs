using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class LokedDoors : MonoBehaviour {

    private const string IS_OPEN = "open";
 
    private Animator animator;
    public bool isOpen = false;
    public GameObject keyNeeded;
    [SerializeField] AudioSource doorSound;
    [SerializeField] AudioSource closedDoorSound;


    private void Awake ( ) {
        animator = GetComponent<Animator>();
 

    }


    public void ChangeDoorState ( ) {
        isOpen = !isOpen;
        
    }

    public void DoorOpen ( ) {
        if (keyNeeded.activeInHierarchy == false)
        {
            closedDoorSound.Play();
        }

        if (keyNeeded.activeInHierarchy == true)
        {
            ChangeDoorState();
        }

        if(keyNeeded.activeInHierarchy == true ) {
            animator.SetBool(IS_OPEN, isOpen);
            doorSound.Play();


        } else if (keyNeeded.activeInHierarchy == true || isOpen == true) {
            animator.SetBool(IS_OPEN, !isOpen);
        }
    }


}
