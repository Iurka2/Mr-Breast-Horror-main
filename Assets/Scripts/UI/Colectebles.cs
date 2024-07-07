using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colectebles : MonoBehaviour
{

    [SerializeField] private AudioSource paperSound;


    


    public void OnCollected ( ) {
        paperSound.Play();
        Destroy(gameObject);
        GameManager.instance?.OnItemCollected();

    }

  
}
