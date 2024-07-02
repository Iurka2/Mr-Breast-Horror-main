using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectedText : MonoBehaviour
{
  
    TMPro.TMP_Text text;
    public int count; 

    private void Awake ( ) {
        text = GetComponent<TMPro.TMP_Text>();
    }

    private void Start ( ) {
        UpdateCount();
    }

   public void onBarColected ( ) {
        count++;
        UpdateCount();
       
    }

    void UpdateCount ( ) {
        text.text = $"{count} / {Colectebles.total}";
    }

}
