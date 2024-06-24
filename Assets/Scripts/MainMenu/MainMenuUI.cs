using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;


    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button closeCreditsBtn;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void Play()
    {
        StartCoroutine(LoadNextScene());
         //PaymentController.Instance.LoadPlayer();
    }

    public void OpenOptions()
    {
        startPanel.SetActive(false);
        OptionsMenu.instance.OpenOptionMenu();
    }

    public void OpenCredits()
    {
        startPanel.SetActive(false);
        creditsPanel.SetActive(true);
        closeCreditsBtn.Select();
    }

    public void OpenStartPanel()
    {
        startPanel.SetActive(true);
        playButton.Select();
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void ResetGame()
    {
        //Reset game
       // SaveSystem.ResetPlayerSave();
       SaveSystem.DeletePlayerSave();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator LoadNextScene()
    {
        if(LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f); // wait for the anim
        Loader.Load(Loader.Scene.GreenHouse);
    }

}
