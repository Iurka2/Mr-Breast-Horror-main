using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Button LVL1;
    [SerializeField] private Button LVL2;
    [SerializeField] private Button LVL3;
    [SerializeField] private Button X;
    [SerializeField] private GameObject levelSelectMenu;


    private void Awake ( ) {
        LVL1.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });

        LVL2.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL1);
        });

        LVL3.onClick.AddListener(( ) => {
            Loader.Load(Loader.Scene.LVL3);
        });

        X.onClick.AddListener(( ) => {
            levelSelectMenu.SetActive(false);
        });
    }

}
