using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;


public class Flashlight : MonoBehaviour {

    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    Light flashLight;
    
    void Start()
    {
        flashLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update( ) {

        flashLight.intensity = Mathf.Clamp(flashLight.intensity, minBrightness, maxBrightness);

        if(drainOverTime == true || flashLight.enabled == true) {
            if (flashLight.intensity > minBrightness) {
                flashLight.intensity -= Time.deltaTime * (drainRate / 1000);
            }
        }

        if(TCKInput.GetAction("FlashBtn", EActionEvent.Down))
        {
            flashLight.enabled = !flashLight.enabled;
        }

    }

    public void RestoreBattery ( float amount) {
        flashLight.intensity += amount;
        
    }


}
