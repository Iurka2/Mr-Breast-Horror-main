using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class LevelSelect : MonoBehaviour {
    [System.Serializable]
    private class Level {
        public Button selectButton;
        public Button playButton;
        public GameObject menu;
        public TextMeshProUGUI bestTimeText;
        public TextMeshProUGUI collectablesText;
        public TextMeshProUGUI secretsText;
        public Image timeMedalImage;
        public Image scoreMedalImage;
        // Add other UI elements as needed
    }

    [SerializeField] private Level[] levels;
    [SerializeField] private Sprite goldMedalSprite;
    [SerializeField] private Sprite silverMedalSprite;
    [SerializeField] private Sprite bronzeMedalSprite;

    private Dictionary<Medal, Sprite> medalSprites;

    private void Awake ( ) {
        medalSprites = new Dictionary<Medal, Sprite> {
            { Medal.Gold, goldMedalSprite },
            { Medal.Silver, silverMedalSprite },
            { Medal.Bronze, bronzeMedalSprite },
            { Medal.None, null }
        };

        for(int i = 0; i < levels.Length; i++) {
            int levelIndex = i + 1; // Levels are 1-indexed
            levels[i].selectButton.onClick.AddListener(( ) => {
                ShowLevelMenu(levelIndex);
                LoadSaveData(levelIndex); // Load data for the level
            });

            levels[i].playButton.onClick.AddListener(( ) => {
                Loader.Load((Loader.Scene)System.Enum.Parse(typeof(Loader.Scene), "LVL" + levelIndex));
            });
        }
    }

    private void ShowLevelMenu ( int levelIndex ) {
        foreach(var level in levels) {
            level.menu.SetActive(false);
        }
        levels[levelIndex - 1].menu.SetActive(true);
    }

    private void LoadSaveData ( int levelIndex ) {
      /*  string filePath = Path.Combine(Application.persistentDataPath, "save_" + levelIndex + ".txt");*/
        string filePath = Path.Combine(Application.dataPath + "/save_" + levelIndex + ".txt");
        Level level = levels[levelIndex - 1];
        if(File.Exists(filePath)) {
            string saveString = File.ReadAllText(filePath);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            level.bestTimeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(saveObject.finalTime / 60), Mathf.FloorToInt(saveObject.finalTime % 60));
            level.collectablesText.text = string.Format("{0}", saveObject.finalScore);
            level.secretsText.text = string.Format("{0}", saveObject.secrets);

            level.timeMedalImage.sprite = GetMedalSprite(saveObject.timeMedal);
            level.scoreMedalImage.sprite = GetMedalSprite(saveObject.scoreMedal);
        } else {
            Debug.LogWarning("Save file for level " + levelIndex + " not found!");
            level.timeMedalImage.gameObject.SetActive(false);
            level.scoreMedalImage.gameObject.SetActive(false);

        }
    }

    private Sprite GetMedalSprite ( Medal medal ) {
        return medalSprites.TryGetValue(medal, out Sprite sprite) ? sprite : null;
    }
}
