using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
    [SerializeField] GameObject onOB;
    [SerializeField] GameObject offOB;




    [SerializeField] GameObject lightOB;


    [SerializeField] AudioSource switchClick;

    public bool lightsOn;


    public void ChangeLightState ( ) {
        lightsOn = !lightsOn;
        switchClick.Play();
    }


    public void Update ( ) {
        offOB.SetActive(false);

        if(lightsOn == true) {
            lightOB.SetActive(true);
            onOB.SetActive(true);
            offOB.SetActive(false);
        } else {
            lightOB.SetActive(false);
            offOB.SetActive(true);
            onOB.SetActive(false);
        }


    }
}