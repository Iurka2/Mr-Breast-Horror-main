using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemsInteractable : MonoBehaviour
{
    public UnityEvent onInteract;
    public int Id;
    




    public void Start ( ) {
        Id = Random.Range(0,9999);
    }




}

