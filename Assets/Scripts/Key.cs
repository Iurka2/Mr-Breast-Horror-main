using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject invObj;
    [SerializeField] GameObject keyObj;
    [SerializeField] AudioSource keySound;
    public static int keyTotal = 1;

 

 
    public void KeyInteract() {
        invObj.SetActive(true);
        keySound.Play();
        GameManager.instance?.OnKeyCollected();
        Destroy(gameObject);
       
    }


}
