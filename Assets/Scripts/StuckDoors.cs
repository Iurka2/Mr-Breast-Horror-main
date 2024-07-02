using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckDoors : MonoBehaviour {

    private const string IS_OPEN = "stuck";
    private Animator animator;  
  
    [SerializeField] AudioSource doorSound;
 

    private void Awake ( ) {
        animator = GetComponent<Animator>();
 

    }


    public void ChangeDoorState ( ) {
        
        doorSound.Play();
        animator.SetTrigger(IS_OPEN);
    }



}
