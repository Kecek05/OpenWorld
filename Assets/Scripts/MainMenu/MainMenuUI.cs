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
    [SerializeField] private Button creditButton;
    [SerializeField] private Button closeCreditButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private LevelFade levelFade;

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
        optionsButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(true);
        });
        closeOptionsMenuButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(false);
        });
        creditButton.onClick.AddListener(() =>
        {
            creditsPanel.SetActive(true);
        });
        closeCreditButton.onClick.AddListener(() =>
        {
            creditsPanel.SetActive(false);
        });
    }

    private IEnumerator LoadNextScene()
    {
        if(levelFade != null)
           levelFade.DoFadeOut();
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.GreenHouse);
        //SceneManager.LoadScene("Greenhouse");
    }

}
