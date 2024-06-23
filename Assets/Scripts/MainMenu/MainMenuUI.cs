using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionsGeralPanelButton;
    [SerializeField] private Button closeOptionsMenuButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button closeCreditButton;
    [SerializeField] private Button bindsPanelButton;
    [SerializeField] private Button optionsPanelButton;

    [Header("Panels")]
    [SerializeField] private GameObject optionsGeralPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject bindsPanel;
    [SerializeField] private LevelFade levelFade;

    [Header("Colors")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;


    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        { // Lambda Expression, C# Delegates
            StartCoroutine(LoadNextScene());
           // PaymentController.Instance.LoadPlayer();
        });
        quitButton.onClick.AddListener(() =>
        { 
            Application.Quit();
        });
        optionsGeralPanelButton.onClick.AddListener(() =>
        {
            optionsGeralPanel.SetActive(true);
        });
        closeOptionsMenuButton.onClick.AddListener(() =>
        {
            optionsGeralPanel.SetActive(false);
        });
        creditButton.onClick.AddListener(() =>
        {
            creditsPanel.SetActive(true);
        });
        closeCreditButton.onClick.AddListener(() =>
        {
            creditsPanel.SetActive(false);
        });
        optionsPanelButton.onClick.AddListener(() =>
        {
            optionsPanelButton.image.color = selectedColor;
            bindsPanelButton.image.color = normalColor;
            bindsPanel.SetActive(false);
            optionsPanel.SetActive(true);
        });
        bindsPanelButton.onClick.AddListener(() =>
        {
            optionsPanelButton.image.color = normalColor;
            bindsPanelButton.image.color = selectedColor;
            optionsPanel.SetActive(false);
            bindsPanel.SetActive(true);
        });
    }

    private IEnumerator LoadNextScene()
    {
        if(levelFade != null)
           levelFade.DoFadeOut();
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.GreenHouse);
    }

}
