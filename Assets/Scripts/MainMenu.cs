using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButon;


    private void Awake ( ) {
        playButon.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });
    }
}
