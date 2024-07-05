using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

    public Button note;


    private void Awake ( ) {
        note.onClick.AddListener(( ) => {
            note.enabled = false;
        });


    }


    }