using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance; // Singleton instance
    public int collectedItems; // Track collected items centrally
    [SerializeField] private int requiredItemsToActivate = 5;
    [SerializeField] private int requiredItemsForKey = 10;// Number of items needed to activate MrBeast

    [SerializeField] private AudioSource EvilLaugh;
    [SerializeField] private AudioSource GameOverSound;

    [SerializeField] TextMeshProUGUI runText;

    [SerializeField] GameObject Gameover;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Key;
    [SerializeField] GameObject MrBeast;

    [SerializeField] private float displayTime = 2f;


    private void Awake ( ) {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Only one GameManager instance allowed
        }
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

    public void GameOver ( ) {
        Gameover.SetActive(true);
        Player.SetActive(false);
        MrBeast.SetActive(false);
        GameOverSound.Play();
    }


    public void Gg( ) {
        Gameover.SetActive(true);
        Player.SetActive(false);
        MrBeast.SetActive(false);
    }

    public void RestartGame ( ) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
