using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public static bool GameIsPaused = false;

    private PlayerInputActions playerInputActions;
    [SerializeField] private GameObject panelPause;

    private void Awake()
    {
        // Inicialize PlayerInputActions
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerInHouse.Enable();
        playerInputActions.PlayerOutSide.Enable();
        playerInputActions.PlayerInHouse.Pause.performed += OnPausePerformed;
        playerInputActions.PlayerOutSide.Pause.performed += OnPausePerformed;

        playerInputActions.PlayerInHouse.Pause.canceled -= OnPausePerformed;
        playerInputActions.PlayerOutSide.Pause.canceled -= OnPausePerformed;
    }

    
    private void OnPausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        GameIsPaused = !GameIsPaused;
        panelPause.SetActive(GameIsPaused);
        Time.timeScale = GameIsPaused ? 0f : 1f;
    }

    private void Update()
    {
        // A lógica de atualização do painel de pausa foi movida para OnPausePerformed
    }
}
