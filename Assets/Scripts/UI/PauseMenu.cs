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

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            //In House
            playerInputActions.PlayerInHouse.Pause.performed += OnPausePerformed;

            //GreenHouse
            playerInputActions.PlayerOutSide.Pause.performed += OnPausePerformed;

            switch (sceneType)
            {
                case Loader.Scene.GreenHouse:
                    playerInputActions.PlayerOutSide.Enable();
                    break;
                case Loader.Scene.DeliveryScene:
                    playerInputActions.PlayerOutSide.Enable();
                    break;
                case Loader.Scene.House:
                    playerInputActions.PlayerInHouse.Enable();
                    break;
            }
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
