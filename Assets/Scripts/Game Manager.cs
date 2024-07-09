using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

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


    private void Awake ( ) {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Only one GameManager instance allowed
        }

        // Load final score and time from PlayerPrefs on Awake
        finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        float savedTime = PlayerPrefs.GetFloat("FinalTime", 0f);
        timer.finalTime = savedTime;
        finalTimerTime.text = string.Format("Final Time: {0:00}:{1:00}", Mathf.FloorToInt(savedTime / 60), Mathf.FloorToInt(savedTime % 60));
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
        finalBars.text = string.Format("Colectebles: {0} / {1}", finalScore, totalScore);

        // Save final score and time to PlayerPrefs on game win
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.SetFloat("FinalTime", timer.elapsedTime);
    }

    public void GameOver ( ) {
        Gameover.SetActive(true);
        Player.SetActive(false);
        MrBeast.SetActive(false);
        GameOverSound.Play();
        Rain.Stop();

        // Reset PlayerPrefs on game over (optional)
        
         PlayerPrefs.DeleteKey("FinalTime");
    }




    public void RestartGame ( ) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Reset PlayerPrefs on restart (optional)
        // PlayerPrefs.DeleteKey("FinalScore");
        // PlayerPrefs.DeleteKey("FinalTime");
    }
}