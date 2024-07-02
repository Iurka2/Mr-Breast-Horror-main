using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject invObj;
    [SerializeField] GameObject keyObj;
    [SerializeField] AudioSource keySound;

    
    private void Start ( ) {
       
    }

 
    public void KeyInteract() {
        invObj.SetActive(true);
        keyObj.SetActive(false);
        keySound.Play();
    }


}
