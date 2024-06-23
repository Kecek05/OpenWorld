using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool GameIsPaused = false;
    [SerializeField] private GameObject panelPause;



    private void Start()
    {
        if(WitchInputs.Instance != null)
            WitchInputs.Instance.OnPausePerformed += WitchInputs_OnPausePerformed;
    }

    private void WitchInputs_OnPausePerformed(object sender, System.EventArgs e)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        GameIsPaused = !GameIsPaused;
        if(GameIsPaused)
        {
            //Pause Game
            panelPause.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            //Un pause
            panelPause.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
