using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColisionTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    private bool hasTriggered = false;
    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            onTriggerEnter.Invoke();
            Debug.Log("entered");
            hasTriggered = true;
        }
    }
    

}
