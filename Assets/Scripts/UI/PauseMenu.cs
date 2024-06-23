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


    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            //Inhouse - create a reference on witchInputs
            playerInputActions.PlayerInHouse.Pause.performed += OnPausePerformed;

            //GreenHouse - create a reference on witchInputs
            playerInputActions.PlayerOutSide.Pause.performed += OnPausePerformed;
        }
    }

    private void OnPausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("apertou o botao");
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
