using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    private const string IS_OPEN = "open";
    private Animator animator;  
    public bool isOpen = false;
    [SerializeField] AudioSource doorSound;
 

    private void Awake ( ) {
        animator = GetComponent<Animator>();
 

    }


    public void ChangeDoorState ( ) {
        isOpen = !isOpen;
        doorSound.Play();
    }

    private void Update ( ) {
        if (isOpen)
        {
            animator.SetBool(IS_OPEN, true);
         

        }
        else
        {
            animator.SetBool(IS_OPEN, false);
           
        }
    }


}
