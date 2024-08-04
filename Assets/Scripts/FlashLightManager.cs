using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public enum FlashLightState {
    Off,
    On,
    Dead
}

[RequireComponent(typeof(AudioSource))]
public class FlashLightManager : MonoBehaviour {
    [Header("Options")]
    [Range(0.0f, 2f)][SerializeField] float batteryLossTick = 0.5f;
    [SerializeField] int startBattery = 100;
    public int currentBattery;
    public FlashLightState state;
    public bool flashLightIsOn;

    [Header("References")]
    [SerializeField] GameObject FlashLight;
    [SerializeField] GameObject FlashLightBattery;
    [SerializeField] AudioClip FlashLightOn_FX, FlashLightOff_FX, ItemPickup;
    [SerializeField] Image flashHealth;

    // Add reference to the Light component
    private Light flashLightComponent;

    private void Start ( ) {
        // Initialize the battery
        currentBattery = startBattery;
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);

        // Find and assign the Light component attached to the FlashLight
        flashLightComponent = FlashLight.GetComponent<Light>();

        if(gameObject.activeInHierarchy == true) {
            GetComponent<AudioSource>().PlayOneShot(ItemPickup);
        }
    }

    private void Update ( ) {
        // Check for flashlight toggle input
        if(TCKInput.GetAction("FlashBtn", EActionEvent.Down)) {
            ToggleFlashLight();
        }

        // Manage flashlight state and visibility
        if(state == FlashLightState.Off) {
            FlashLight.SetActive(false);
        } else if(state == FlashLightState.On) {
            FlashLight.SetActive(true);
            // Adjust light intensity based on battery level
            flashLightComponent.intensity = Mathf.Lerp(0, 1, currentBattery / (float)startBattery);
        } else if(state == FlashLightState.Dead) {
            FlashLight.SetActive(false);
        }

        // Update flashlight state if the battery is depleted
        if(currentBattery <= 0) {
            currentBattery = 0;
            state = FlashLightState.Dead;
            flashLightIsOn = false;
        }

        // Update the UI to reflect the battery level
        flashHealth.fillAmount = currentBattery / 100f;
    }

    public void GainBattery ( int amount ) {
        // Revive flashlight if battery is refilled
        if(currentBattery == 0) {
            state = FlashLightState.On;
            flashLightIsOn = true;
        }

        // Cap the battery to startBattery limit
        if(currentBattery + amount > startBattery) {
            currentBattery = startBattery;
        } else {
            currentBattery += amount;
        }
    }

    private void LoseBattery ( ) {
        if(state == FlashLightState.On) {
            currentBattery--;
            flashHealth.fillAmount = currentBattery / 100f;
        }
    }

    public void ToggleFlashLight ( ) {
        flashLightIsOn = !flashLightIsOn;
        if(state == FlashLightState.Dead) {
            flashLightIsOn = false;
        }

        if(flashLightIsOn) {
            GetComponent<AudioSource>().PlayOneShot(FlashLightOn_FX);
            state = FlashLightState.On;
            FlashLightBattery.SetActive(true);
        } else {
            GetComponent<AudioSource>().PlayOneShot(FlashLightOff_FX);
            state = FlashLightState.Off;
        }
    }
}
