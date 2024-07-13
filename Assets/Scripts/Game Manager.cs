using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;
using static GameManager;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance; // Singleton instance
    public int collectedItems;
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

    public List<SaveObject> saveObjects = new List<SaveObject>();


    private void Awake ( ) {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Only one GameManager instance allowed
        }
        Time.timeScale = 1;

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

        if(existingSaveData == null || timer.finalTime < existingSaveData.finalTime || finalScore > existingSaveData.finalScore) {
            // Create new save data if better
            SaveObject levelSaveData = new SaveObject {
                finalTime = timer.finalTime,
                finalScore = finalScore,
                levelName = SceneManager.GetActiveScene().buildIndex
            };

            // Add the level data to the list
            saveObjects.Add(levelSaveData);

            string json = JsonUtility.ToJson(levelSaveData); // Save all level data
            File.WriteAllText(Application.dataPath + "/save_" + levelSaveData.levelName + ".txt", json);
        }
    }

    private SaveObject LoadSaveData ( int levelIndex ) {
        string filePath = Application.dataPath + "/save_" + levelIndex + ".txt";

        if(File.Exists(filePath)) {
            string saveString = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveObject>(saveString);
        }

        return null;
    }





    public void GameOver ( ) {
        Gameover.SetActive(true);
        Player.SetActive(false);
        MrBeast.SetActive(false);
        GameOverSound.Play();
        Rain.Stop();
      
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
    }
}




// Save final score and time to PlayerPrefs on game win
/*        if(finalScore > PlayerPrefs.GetInt("FinalScore", 0)) {
            PlayerPrefs.SetInt("FinalScore", finalScore);
        }
        if (PlayerPrefs.HasKey("FinalTime"))
        {
            Debug.Log("there is save");
            if(timer.finalTime < PlayerPrefs.GetFloat("FinalTime", 0f)) {
                PlayerPrefs.SetFloat("FinalTime", timer.finalTime);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("FinalTime", timer.finalTime);
            Debug.Log("no save" + PlayerPrefs.GetFloat("FinalTime", 0f));

        }*/

