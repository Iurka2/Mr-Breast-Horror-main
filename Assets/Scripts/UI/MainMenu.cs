using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButon;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button levelSelect;
    [SerializeField] private GameObject levelSelectMenu;


    private void Awake ( ) {
        playButon.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });

        levelSelect.onClick.AddListener(( ) => {
            levelSelectMenu.SetActive(true);
        });

        quitButton.onClick.AddListener(( ) => {
            Application.Quit();
        });
    }
}
