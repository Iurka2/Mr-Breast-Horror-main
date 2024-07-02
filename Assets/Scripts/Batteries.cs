using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour {

    [Header("Options")]
    [SerializeField] int batteryWeight = 25;
    public AudioSource batterySound;


    public void OnBateriesInteract ( ) {
        FindAnyObjectByType<FlashLightManager>().GainBattery(batteryWeight);
        batterySound.Play();
    }


 
}
