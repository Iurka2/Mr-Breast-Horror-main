using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [SerializeField] private Button playButon;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;


    private void Awake ( ) {
        mainMenuButton.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        playButon.onClick.AddListener(( ) => {
            ResumeGame();
        });

        quitButton.onClick.AddListener(( ) => {
            Application.Quit();
        });

        pauseButton.onClick.AddListener(( ) => {
            PauseGame();
        });
    }


    public void PauseGame ( ) {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame ( ) {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
}
