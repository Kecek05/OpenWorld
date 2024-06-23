using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public bool GameIsPaused = false;
    [SerializeField] private GameObject panelPause;


    private PlayerInputActions playerInputActions;
    public Loader.Scene sceneType;

    private void Start()
    {
        MainMenuUI.OnCloseOptions += MainMenuUI_OnCloseOptions;
    }




    private void MainMenuUI_OnCloseOptions()
    {
        OnPausePerformed();
    }

    private void OnPausePerformed()
    {
        Debug.Log("despause");
        if (GameIsPaused)
        {
            panelPause.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            panelPause.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
}
