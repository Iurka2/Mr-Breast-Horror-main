using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectedTextKey : MonoBehaviour
{
  
    TMPro.TMP_Text text;
    public int count; 

    private void Awake ( ) {
        text = GetComponent<TMPro.TMP_Text>();
    }

    private void Start ( ) {
        UpdateCount();
    }

   public void onKeyColected ( ) {
        count++;
        UpdateCount();
       
    }

    void UpdateCount ( ) {
        text.text = $"{count} / {Key.keyTotal}";
    }

}
