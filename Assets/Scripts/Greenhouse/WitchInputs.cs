using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchInputs : MonoBehaviour
{
    public static WitchInputs main;

    PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private bool run;
    [SerializeField] private bool jump;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        run = false;
        jump = false;
    }

    private void OnEnable()
    {
        if(playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
            //playerInputActions.PlayerMovement.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerMovement.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerMovement.Move.canceled += OnMovementCanceled;
            playerInputActions.PlayerMovement.Run.performed += i => run = true;
            playerInputActions.PlayerMovement.Run.canceled += i => run = false;
            playerInputActions.PlayerMovement.Jump.performed += i => jump = true;
            playerInputActions.PlayerMovement.Jump.canceled += i => jump = false;
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

    public bool GetRunInput() { return run;  }
    public bool GetJumpInput() { return jump; }
    public void GetAllInputs() => HandleAllInputs();

}
