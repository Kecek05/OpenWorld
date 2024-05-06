using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();

        playerInputActions.PlayerMovement.Interact.performed += Interact_performed;
        playerInputActions.PlayerMovement.InteractAlternate.performed += InteractAlternate_performed;
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


}
