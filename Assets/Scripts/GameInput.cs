using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnHit1Performed;
    public event EventHandler OnHit2Performed;
    public event EventHandler OnHit3Performed;
    public event EventHandler OnHit4Performed;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Interact.performed += Interact_performed;
        playerInputActions.PlayerMovement.InteractAlternate.performed += InteractAlternate_performed;

        playerInputActions.PlayerHitMinigame.Hit1.performed += Hit1_performed;
        playerInputActions.PlayerHitMinigame.Hit2.performed += Hit2_performed;
        playerInputActions.PlayerHitMinigame.Hit3.performed += Hit3_performed;
        playerInputActions.PlayerHitMinigame.Hit4.performed += Hit4_performed;
        playerInputActions.PlayerHitMinigame.Disable();
    }


    private void Hit1_performed(InputAction.CallbackContext obj)
    {
        OnHit1Performed?.Invoke(this,EventArgs.Empty);
    }

    private void Hit2_performed(InputAction.CallbackContext obj)
    {
        OnHit2Performed?.Invoke(this, EventArgs.Empty);
    }

    private void Hit3_performed(InputAction.CallbackContext obj)
    {
        OnHit3Performed?.Invoke(this, EventArgs.Empty);
    }

    private void Hit4_performed(InputAction.CallbackContext obj)
    {
        OnHit4Performed?.Invoke(this, EventArgs.Empty);
    }


    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); // se for null ele nao faz nada, se nao for ele faz o Invoke
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.PlayerMovement.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        ////arredonda o valor para 0 se ele for menor que 0.4 (isso melhora o movimento pelo controle)
        if ((inputVector.x < 0.4f && inputVector.x > 0f) || (inputVector.x > -0.4f && inputVector.x < 0f))
            inputVector.x = 0f;
        if ((inputVector.y < 0.4f && inputVector.y > 0f) || (inputVector.y > -0.4f && inputVector.y < 0f))
            inputVector.y = 0f;
        return inputVector;
    }

    public void ChangePLayerInputHitMinigame(bool state)
    {
        if (state)
        {
            playerInputActions.PlayerMovement.Disable();
            playerInputActions.PlayerHitMinigame.Enable();
        }
        else
        {
            playerInputActions.PlayerHitMinigame.Disable();
            playerInputActions.PlayerMovement.Enable();
        }

        Debug.Log("Player Movement is: " + playerInputActions.PlayerMovement.enabled);

        Debug.Log("Player HitMinigame is: " + playerInputActions.PlayerHitMinigame.enabled);
    }
}
