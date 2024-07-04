using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public enum FlashLightState {
    Off,
    On,
    Dead
}

[RequireComponent(typeof(AudioSource))]
public class FlashLightManager : MonoBehaviour
{
    [Header("Options")]
    [Range(0.0f, 2f)] [SerializeField] float batteryLossTick = 0.5f;
    [SerializeField] int startBattery = 100;
    public int currentBattery;
    public FlashLightState state;
    public bool flashLightIsOn;

    [Header("References")]
    [SerializeField] GameObject FlashLight;
    [SerializeField] GameObject FlashLightBattery;
    [SerializeField] AudioClip FlashLightOn_FX, FlashLightOff_FX,ItemPickup;
    [SerializeField] Image flashHealth;

    private void Start ( ) {
        currentBattery = startBattery;
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);

        if(gameObject.activeInHierarchy == true) {
            GetComponent<AudioSource>().PlayOneShot(ItemPickup);
        }
        

    }

    private void Update ( ) {

        if(TCKInput.GetAction("FlashBtn", EActionEvent.Down)) {
            ToggleFlashLight();
        }

        if(state == FlashLightState.Off) {
            FlashLight.SetActive(false);
        } else if (state == FlashLightState.On) {
            FlashLight.SetActive(true);
        } else if (state == FlashLightState.Dead) {
            FlashLight.SetActive(false);
        }

        if(currentBattery <= 0) {
            currentBattery = 0;
            state = FlashLightState.Dead;
            flashLightIsOn = false;
        }

        flashHealth.fillAmount = currentBattery / 100f;

    }

    public void GainBattery (int amount ) {

        if (currentBattery == 0)
        {
            state = FlashLightState.On;
            flashLightIsOn = true;
        }

        if (currentBattery + amount > startBattery) 
        { currentBattery = startBattery; } 
        else { currentBattery += amount; }


    }

    private void LoseBattery ( ) {
        if (state == FlashLightState.On)
        {
            currentBattery--;
            flashHealth.fillAmount = currentBattery / 100f;
        }
    }

    public void ToggleFlashLight ( ) {
        flashLightIsOn = !flashLightIsOn;
        if (state == FlashLightState.Dead)
        {
            flashLightIsOn = false;
        }

        if (flashLightIsOn)
        {
            GetComponent<AudioSource>().PlayOneShot(FlashLightOn_FX);
            state = FlashLightState.On;
            FlashLightBattery.SetActive(true);
        } else {
            GetComponent<AudioSource>().PlayOneShot(FlashLightOff_FX);
            state = FlashLightState.Off;
        }

     
    }






}
