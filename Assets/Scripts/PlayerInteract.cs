using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;




public class PlayerInteract : MonoBehaviour {
    public LayerMask interactableLayerMask = 8;
    ItemsInteractable interactable;
    [SerializeField] GameObject interactText;
    public GameObject keyNeeded;
    public float maxDistance = 1.55f;
  



    private void Update ( ) {
     
        Interaction();

    }
    public RaycastHit hit;
    public void Interaction () {
     

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,maxDistance, interactableLayerMask)) {
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

