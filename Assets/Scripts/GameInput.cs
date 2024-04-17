using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.PlayerMovement.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        Debug.Log(inputVector);
        return inputVector;
    }
}
