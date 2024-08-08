using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Remote : MonoBehaviour {
    



    [SerializeField] GameObject TV;

    [SerializeField] AudioSource switchClick;

    public bool lightsOn;


    public void ChangeLightState ( ) {
        lightsOn = !lightsOn;
        switchClick.Play();
    }


    public void Update ( ) {
   

        if(lightsOn == true) {
            TV.SetActive(true);
         
        } else {
            TV.SetActive(false);
      
        }


    }
}