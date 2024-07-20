using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.InputSystem.HID;
using Unity.VisualScripting.Antlr3.Runtime;


public class PlayerInteract : MonoBehaviour {
    public LayerMask interactableLayerMask = 8;
    ItemsInteractable interactable;
    [SerializeField] GameObject interactText;
    public GameObject keyNeeded;
  



    private void Update ( ) {
     
        Interaction();

    }
    public RaycastHit hit;
    public void Interaction () {
     

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,1.55f, interactableLayerMask)) {
            interactText.SetActive(true);
            if(hit.collider.GetComponent<ItemsInteractable>() != false) {

                if(interactable == null || interactable.Id != hit.collider.GetComponent<ItemsInteractable>().Id) {
                    interactable = hit.collider.GetComponent<ItemsInteractable>();
                } 
                
            
                if(TCKInput.GetAction("interactBtn", EActionEvent.Down)) {

                    Debug.Log("hit");
                    interactable.onInteract.Invoke();
                  
                }
              
            }
        } else {
            interactText.SetActive(false);
        }
    }
}

