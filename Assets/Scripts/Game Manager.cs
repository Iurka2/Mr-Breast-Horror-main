using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour {

    public static GameManager instance; // Singleton instance
    public int collectedItems;
    public int collectedSecrets;
    public int keycount;// Track collected items centrally
    public int totalScore;
    public int finalScore;
    [SerializeField] private int requiredItemsToActivate = 5;
    [SerializeField] private int requiredItemsForKey = 10;// Number of items needed to activate MrBeast

    [SerializeField] private AudioSource EvilLaugh;
    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource Rain;

    [SerializeField] TextMeshProUGUI runText;
    [SerializeField] TextMeshProUGUI finalTimerTime;
    [SerializeField] TextMeshProUGUI finalBars;

    [SerializeField] GameObject Gameover;
    [SerializeField] GameObject GG;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Key;
    [SerializeField] GameObject MrBeast;
    [SerializeField] TimerTimer timer;
    [SerializeField] ColectedText colectedText;
    [SerializeField] private float displayTime = 2f;

    [SerializeField] private PlayableDirector gameOverCutscene; // Reference to the PlayableDirector component

    public List<SaveObject> saveObjects = new List<SaveObject>();

    private void Awake ( ) {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Only one GameManager instance allowed
        }
        Time.timeScale = 1;

        Screen.SetResolution(480, 240, true);
    }

    private void Start ( ) {
        gameOverCutscene.stopped += OnCutsceneStopped; // Subscribe to the stopped event
    }

    private void OnDestroy ( ) {
        gameOverCutscene.stopped -= OnCutsceneStopped; // Unsubscribe from the stopped event
    }

    public void OnItemCollected ( ) // Update collected items from other scripts
    {
        collectedItems++;
        if(collectedItems == requiredItemsToActivate) {
            ActivateMrBeast();
            TextApearing();
        }

        if(collectedItems == requiredItemsForKey) {
            Key.SetActive(true);
        }
        finalScore = collectedItems;
    }

    public void onSecretColected ( ) {
        collectedSecrets++;
    }

    public void OnKeyCollected ( ) {
        keycount++;
    }

    private void ActivateMrBeast ( ) {
        MrBeast.SetActive(true);
        EvilLaugh.Play();
    }

    private void TextApearing ( ) {
        runText.enabled = true;
        StartCoroutine(HideTextAfterDelay());
    }

    private IEnumerator HideTextAfterDelay ( ) {
        yield return new WaitForSeconds(displayTime); // Wait for the specified time
        runText.enabled = false; // Disable the text
    }


    public void Gg ( ) {
        GG.SetActive(true);
        Player.SetActive(false);
        MrBeast.SetActive(false);
        Rain.Stop();
        timer.StopTimer();

        finalTimerTime.text = string.Format("Final Time: {0:00}:{1:00}", Mathf.FloorToInt(timer.elapsedTime / 60), Mathf.FloorToInt(timer.elapsedTime % 60));
        finalBars.text = string.Format("Collectables: {0} / {1}", finalScore, totalScore);

        // Load existing data to compare
        SaveObject existingSaveData = LoadSaveData(SceneManager.GetActiveScene().buildIndex);

        bool newBestTime = existingSaveData == null || timer.finalTime < existingSaveData.finalTime;
        bool newBestScore = existingSaveData == null || finalScore > existingSaveData.finalScore;
        bool newBestSecrets = existingSaveData == null || collectedSecrets > existingSaveData.secrets;

        if(newBestTime || newBestScore) {
            // Update save data only if there is a new best time or score
            SaveObject levelSaveData = new SaveObject {
                finalTime = newBestTime ? timer.finalTime : existingSaveData.finalTime,
                finalScore = newBestScore ? finalScore : existingSaveData.finalScore,
                secrets = newBestSecrets ? collectedSecrets : existingSaveData.secrets,
                levelName = SceneManager.GetActiveScene().buildIndex
            };

            string json = JsonUtility.ToJson(levelSaveData); // Save all level data
            string filePath = Path.Combine(Application.persistentDataPath, "save_" + levelSaveData.levelName + ".txt");
            File.WriteAllText(filePath, json);
        }
    }

    private SaveObject LoadSaveData ( int levelIndex ) {
        string filePath = Path.Combine(Application.persistentDataPath, "save_" + levelIndex + ".txt");

        if(File.Exists(filePath)) {
            string saveString = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveObject>(saveString);
        }

        return null;
    }

    public void GameOver ( ) {
       

        
        
        MrBeast.SetActive(false);
        GameOverSound.Play();
        Rain.Stop();

        // Play the cutscene
        gameOverCutscene.Play();
    }

    private void OnCutsceneStopped ( PlayableDirector director ) {
        if(director == gameOverCutscene) {
            Gameover.SetActive(true);
        }
    }

    public void RestartGame ( ) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu ( ) {
        SceneManager.LoadScene("MainMenu");
    }

    [Serializable]
    public class SaveObject {
        public float finalTime;
        public int finalScore;
        public int levelName;
        public int secrets;
    }
}
