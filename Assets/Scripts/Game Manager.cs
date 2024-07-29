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
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class GameManager : MonoBehaviour {

    [SerializeField] private LevelData[] levelDataArray; // Array to hold level data


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
    [SerializeField] TextMeshProUGUI goToExitText;
    [SerializeField] TextMeshProUGUI finalTimerTime;
    [SerializeField] TextMeshProUGUI finalBars;
    [SerializeField] TextMeshProUGUI newBest;
    [SerializeField] TextMeshProUGUI newBest2;

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
    public enum Medal {
        None,
        Bronze,
        Silver,
        Gold
    }



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
    private IEnumerator HideTextAfterDelay ( float delay ) {
        yield return new WaitForSeconds(delay);
        goToExitText.enabled = false;
    }
    public void OnKeyCollected ( ) {
        keycount++;
        goToExitText.enabled = true;
        StartCoroutine(HideTextAfterDelay(displayTime));
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

        int currentLevel = SceneManager.GetActiveScene().buildIndex;


        Medal timeMedal = CalculateTimeMedal(timer.finalTime, currentLevel);
        Medal scoreMedal = CalculateScoreMedal(finalScore, currentLevel);


        // Load existing data to compare
        SaveObject existingSaveData = LoadSaveData(SceneManager.GetActiveScene().buildIndex);

        bool newBestTime = existingSaveData == null || timer.finalTime < existingSaveData.finalTime;
        bool newBestScore = existingSaveData == null || finalScore > existingSaveData.finalScore;
        bool newBestSecrets = existingSaveData == null || collectedSecrets > existingSaveData.secrets;




        if(newBestTime || newBestScore || newBestSecrets) {
            // Update save data only if there is a new best time or score
            SaveObject levelSaveData = new SaveObject {
                finalTime = newBestTime ? timer.finalTime : existingSaveData.finalTime,
                finalScore = newBestScore ? finalScore : existingSaveData.finalScore,
                secrets = newBestSecrets ? collectedSecrets : existingSaveData.secrets,
                levelName = SceneManager.GetActiveScene().buildIndex,
                timeMedal = newBestTime ? timeMedal : existingSaveData.timeMedal,
                scoreMedal = newBestScore ? scoreMedal : existingSaveData.scoreMedal,
               
            };

            string json = JsonUtility.ToJson(levelSaveData); // Save all level data

            string filePath = Path.Combine(Application.persistentDataPath, "save_" + levelSaveData.levelName + ".txt");

            File.WriteAllText(filePath, json);

        }
        PublishingEvents.events.Win.Invoke();
    }



    private Medal CalculateTimeMedal ( float finalTime, int levelIndex ) {
        LevelData levelData = GetLevelData(levelIndex);
        if(finalTime < levelData.goldTime) return Medal.Gold;
        if(finalTime < levelData.silverTime) return Medal.Silver;
        return Medal.Bronze;
    }

    private Medal CalculateScoreMedal ( int score, int levelIndex ) {
        LevelData levelData = GetLevelData(levelIndex);
        if(score >= levelData.goldScore) return Medal.Gold;
        if(score >= levelData.silverScore) return Medal.Silver;
        return Medal.Bronze;
    }

    private LevelData GetLevelData ( int levelIndex ) {
        foreach(var levelData in levelDataArray) {
            if(levelData.levelNumber == levelIndex) {
                return levelData;
            }
        }
        return null;
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
        PublishingEvents.events.Lost.Invoke();

    }

    private void OnCutsceneStopped ( PlayableDirector director ) {
        if(director == gameOverCutscene) {
            Gameover.SetActive(true);
        }
    }

    public void RestartGame ( ) {
        PublishingEvents.events.Interstitial.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu ( ) {
        PublishingEvents.events.Interstitial.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    [Serializable]
    public class SaveObject {
        public float finalTime;
        public int finalScore;
        public int levelName;
        public int secrets;
        public Medal timeMedal;  // Medal for time
        public Medal scoreMedal; // Medal for score
       
    }
}
