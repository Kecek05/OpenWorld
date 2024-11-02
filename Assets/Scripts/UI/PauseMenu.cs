using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool GameIsPaused = false;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button settingBtn;
    private IEnumerator mainMenuCoroutine;

    
    private void Start()
    {
        Invoke(nameof(CheckingWitchInputs), 1f);
    }

    private void WitchInputs_OnPausePerformed(object sender, System.EventArgs e)
    {
        PauseGame();
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(true);
        }
    }

    private void CheckingWitchInputs()
    {
        if (WitchInputs.Instance != null)
        {
            WitchInputs.Instance.OnPausePerformed += WitchInputs_OnPausePerformed;
        }
    }


    public void PauseGame()
    {
        GameIsPaused = !GameIsPaused;
        if (GameIsPaused)
        {
            // Pause Game
            if (panelPause != null)
            {
                panelPause.SetActive(true);
            }
            if (settingBtn != null)
            {
                settingBtn.Select();
            }
            Time.timeScale = 0f;
        }
        else
        {
            // Unpause Game
            if (panelPause != null)
            {
                panelPause.SetActive(false);
            }
            Time.timeScale = 1f;
        }
        //if(GameIsPaused)
        //{
        //    //Pause Game
        //    panelPause.SetActive(true);
        //    settingBtn.Select();
        //    Time.timeScale = 0f;
        //} else
        //{
        //    //Un pause
        //    panelPause.SetActive(false);
        //    Time.timeScale = 1f;
        //}
    }


    public void QuitGame()
    {
        PauseGame();
        Application.Quit();
    }


    public void MainMenuButton()
    {
        if(mainMenuCoroutine == null)
        {
            WitchInputs.Instance.ChangeActiveMap(Loader.Scene.GreenHouse);
            mainMenuCoroutine = MainMenu();
            StartCoroutine(mainMenuCoroutine);
        }
    }

    public IEnumerator MainMenu()
    {
        PauseGame();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
