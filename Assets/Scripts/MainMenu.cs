using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButon;
    [SerializeField] private Button quitButton;


    private void Awake ( ) {
        playButon.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });

        quitButton.onClick.AddListener(( ) => {
            Application.Quit();
        });
    }
}
