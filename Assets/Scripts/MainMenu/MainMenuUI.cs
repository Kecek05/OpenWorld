using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button closeOptionsMenuButton;
    [SerializeField] private GameObject optionsPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        { // Lambda Expression, C# Delegates
            Loader.Load(Loader.Scene.GreenHouse);
            PaymentController.Instance.LoadPlayer();
        });
        quitButton.onClick.AddListener(() =>
        { 
            Application.Quit();
        });
        optionsButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(true);
        });
        closeOptionsMenuButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(false);
        });
    }



}