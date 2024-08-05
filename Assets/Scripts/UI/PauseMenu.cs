using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;

    [SerializeField] private Button playButon;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartlvl;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private AudioSource[] audioSources;

    private void Awake ( ) {
        // Cache all audio sources in the scene
        audioSources = FindObjectsOfType<AudioSource>();

        mainMenuButton.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        restartlvl.onClick.AddListener(( ) => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        MuteAllAudio();
    }

    public void ResumeGame ( ) {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        UnmuteAllAudio();
    }

    private void MuteAllAudio ( ) {
        foreach(var audioSource in audioSources) {
            if(audioSource != null && audioSource.isPlaying) {
                audioSource.Pause();
            }
        }
    }

    private void UnmuteAllAudio ( ) {
        foreach(var audioSource in audioSources) {
            if(audioSource != null) {
                audioSource.UnPause();
            }
        }
    }
}
