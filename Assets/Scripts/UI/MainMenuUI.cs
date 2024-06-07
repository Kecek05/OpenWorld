using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        { // Lambda Expression, C# Delegates
            Loader.Load(Loader.Scene.GreenHouse);
        });
        quitButton.onClick.AddListener(() =>
        { 
            Application.Quit();
        });
    }



}
