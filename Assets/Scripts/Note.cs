using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class Note : MonoBehaviour {

    public GameObject note;


 
    private void Update ( ) {
        NoteAction();

    }

    private void NoteAction ( ) {
        if(TCKInput.GetAction("Note", EActionEvent.Down)) {
            note.SetActive(false);
            
        }
    }




}