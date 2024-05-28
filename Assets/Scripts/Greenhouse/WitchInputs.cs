using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchInputs : MonoBehaviour
{
    public static WitchInputs main;

    PlayerInputActions playerInputActions;
    [SerializeField] private Vector2 movementInput;
    private float verticalInput;
    private float horizontalInput;


    private void Awake()
    {
        main = this;
    }

    private void OnEnable()
    {
        if(playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
           // playerInputActions.PlayerMovement.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerMovement.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerMovement.Move.canceled += OnMovementCanceled;
        }
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    
    public float GetVerticalInput() { return verticalInput; }
    public float GetHorizontalInput() { return horizontalInput; }

    public void GetAllInputs() => HandleAllInputs();

}
