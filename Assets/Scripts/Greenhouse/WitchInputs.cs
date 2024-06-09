using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchInputs : MonoBehaviour
{
    public static WitchInputs Instance { get; private set; }


    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnHit1Performed;
    public event EventHandler OnHit2Performed;
    public event EventHandler OnHit3Performed;
    public event EventHandler OnHit4Performed;


    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private bool run;
    [SerializeField] private bool jump;

    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<PlayerInput>();

        Debug.Log("Action map is: " + playerInput.currentActionMap);
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
            //Movement
            playerInputActions.PlayerMovement.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerMovement.Move.canceled += OnMovementCanceled;
            playerInputActions.PlayerMovement.Run.performed += i => run = true;
            playerInputActions.PlayerMovement.Run.canceled += i => run = false;
            playerInputActions.PlayerMovement.Jump.performed += i => jump = true;
            playerInputActions.PlayerMovement.Jump.canceled += i => jump = false;
            playerInputActions.PlayerMovement.Enable();

            //Hit Minigame
            playerInputActions.PlayerHitMinigame.Hit1.performed += Hit1_performed;
            playerInputActions.PlayerHitMinigame.Hit2.performed += Hit2_performed;
            playerInputActions.PlayerHitMinigame.Hit3.performed += Hit3_performed;
            playerInputActions.PlayerHitMinigame.Hit4.performed += Hit4_performed;
            playerInputActions.PlayerHitMinigame.Disable();

            //Interact
            playerInputActions.PlayerMovement.Interact.performed += Interact_performed;
            playerInputActions.PlayerMovement.InteractAlternate.performed += InteractAlternate_performed;
        }
 
    }

    private void OnDisable()
    {
       // playerInputActions.Disable();
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); // se for null ele nao faz nada, se nao for ele faz o Invoke
    }


    private void Hit1_performed(InputAction.CallbackContext obj)
    {
        OnHit1Performed?.Invoke(this, EventArgs.Empty);
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


    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    public void ChangePLayerInputHitMinigame(bool state)
    {
        if (state)
        {
            playerInputActions.PlayerMovement.Disable();
            playerInputActions.PlayerHitMinigame.Enable();
            playerInput.SwitchCurrentActionMap("PlayerHitMinigame");
        }
        else
        {
            playerInputActions.PlayerHitMinigame.Disable();
            playerInputActions.PlayerMovement.Enable();
            playerInput.SwitchCurrentActionMap("PlayerMovement");
        }

        Debug.Log("Player Movement is: " + playerInputActions.PlayerMovement.enabled);

        Debug.Log("Player HitMinigame is: " + playerInputActions.PlayerHitMinigame.enabled);

        Debug.Log("Action map is: " + playerInput.currentActionMap);
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
