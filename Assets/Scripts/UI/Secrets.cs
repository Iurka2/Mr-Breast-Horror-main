using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Secrets : MonoBehaviour
{

    [SerializeField] private AudioSource secretsSound;


    


    public void OnSecretCollected ( ) {
        secretsSound.Play();
        Destroy(gameObject);
        GameManager.instance?.onSecretColected();

    }

  
}
