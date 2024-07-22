using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class LevelSelect : MonoBehaviour {
    [SerializeField] private Button LVL1;
    [SerializeField] private Button LVL2;
    [SerializeField] private Button LVL3;

    [SerializeField] private Button LVL1Play;
    [SerializeField] private Button LVL2Play;
    [SerializeField] private Button LVL3Play;


    
    [SerializeField] private GameObject LVL1Menu;
    [SerializeField] private GameObject LVL2Menu;
    [SerializeField] private GameObject LVL3Menu;

    // Use dictionaries to store UI elements for each level
    private Dictionary<int, GameObject> levelMenus = new Dictionary<int, GameObject>();
    private Dictionary<int, TextMeshProUGUI> bestTimes = new Dictionary<int, TextMeshProUGUI>();
    private Dictionary<int, TextMeshProUGUI> collectables = new Dictionary<int, TextMeshProUGUI>();
    private Dictionary<int, TextMeshProUGUI> secrets = new Dictionary<int, TextMeshProUGUI>();

    private void Awake ( ) {
        // Initialize the dictionaries with level-specific UI elements
        levelMenus[1] = LVL1Menu;
        levelMenus[2] = LVL2Menu;
        levelMenus[3] = LVL3Menu;

        bestTimes[1] = LVL1Menu.transform.Find("BestTime_score").GetComponent<TextMeshProUGUI>();
        bestTimes[2] = LVL2Menu.transform.Find("BestTime_score").GetComponent<TextMeshProUGUI>();
        bestTimes[3] = LVL3Menu.transform.Find("BestTime_score").GetComponent<TextMeshProUGUI>();

        collectables[1] = LVL1Menu.transform.Find("Colectables_score").GetComponent<TextMeshProUGUI>();
        collectables[2] = LVL2Menu.transform.Find("Colectables_score").GetComponent<TextMeshProUGUI>();
        collectables[3] = LVL3Menu.transform.Find("Colectables_score").GetComponent<TextMeshProUGUI>();

        secrets[1] = LVL1Menu.transform.Find("Secrets_Score").GetComponent<TextMeshProUGUI>();
        secrets[2] = LVL2Menu.transform.Find("Secrets_Score").GetComponent<TextMeshProUGUI>();
        secrets[3] = LVL3Menu.transform.Find("Secrets_Score").GetComponent<TextMeshProUGUI>();

        LVL1.onClick.AddListener(( ) => {
            ShowLevelMenu(1);
            LoadSaveData(1); // Load data for level 1
        });

        LVL2.onClick.AddListener(( ) => {
            ShowLevelMenu(2);
            LoadSaveData(2); // Load data for level 2
        });

        LVL3.onClick.AddListener(( ) => {
            ShowLevelMenu(3);
            LoadSaveData(3); // Load data for level 3
        });

        LVL1Play.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });

/*        LVL2Play.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL2);
        });

        LVL3Play.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL3);
        });*/

    }

    private void ShowLevelMenu ( int levelIndex ) {
        // Hide all menus first
        foreach(var menu in levelMenus.Values) {
            menu.SetActive(false);
        }
        // Show the selected menu
        levelMenus[levelIndex].SetActive(true);
    }

    private void LoadSaveData ( int levelIndex ) {
        string filePath = Path.Combine(Application.persistentDataPath, "save_" + levelIndex + ".txt");

        if(File.Exists(filePath)) {
            string saveString = File.ReadAllText(filePath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            // Update UI elements with the loaded data
            bestTimes[levelIndex].text = string.Format(" {0:00}:{1:00}", Mathf.FloorToInt(saveObject.finalTime / 60), Mathf.FloorToInt(saveObject.finalTime % 60));
            collectables[levelIndex].text = string.Format("{0}", saveObject.finalScore);
            secrets[levelIndex].text = string.Format("{0}", saveObject.secrets);
        } else {
            Debug.LogWarning("Save file for level " + levelIndex + " not found!");
        }
    }
}
