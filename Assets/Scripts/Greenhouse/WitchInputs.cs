using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchInputs : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";


    public static WitchInputs Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnHit1Performed;
    public event EventHandler OnHit2Performed;
    public event EventHandler OnHit3Performed;
    public event EventHandler OnHit4Performed;


    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private float verticalInput;
    private float horizontalInput;

    private bool run;
    private bool jump;

    public Loader.Scene sceneType;

    public enum Binding
    {
        Move_Up, 
        Move_Down, 
        Move_Left, 
        Move_Right,
        Jump,
        Interact,
        Alternate_Interact
    }


    private void Awake()
    {
        Instance = this;

        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }

            //OutSide
            playerInputActions.PlayerOutSide.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerOutSide.Move.canceled += OnMovementCanceled;
            playerInputActions.PlayerOutSide.Run.performed += i => run = true;
            playerInputActions.PlayerOutSide.Run.canceled += i => run = false;
            playerInputActions.PlayerOutSide.Jump.performed += i => jump = true;
            playerInputActions.PlayerOutSide.Jump.canceled += i => jump = false;
            playerInputActions.PlayerOutSide.Interact.performed += Interact_performed;
            ChangeMovement(false);

            //Hit Minigame
            playerInputActions.PlayerHitMinigame.Hit1.performed += Hit1_performed;
            playerInputActions.PlayerHitMinigame.Hit2.performed += Hit2_performed;
            playerInputActions.PlayerHitMinigame.Hit3.performed += Hit3_performed;
            playerInputActions.PlayerHitMinigame.Hit4.performed += Hit4_performed;
            playerInputActions.PlayerHitMinigame.Disable();

            //In Side
            playerInputActions.PlayerInHouse.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerInHouse.Move.canceled += OnMovementCanceled;
            playerInputActions.PlayerInHouse.Dash.performed += i => run = true;
            playerInputActions.PlayerInHouse.Dash.canceled += i => run = false;
            playerInputActions.PlayerInHouse.Interact.performed += Interact_performed;
            playerInputActions.PlayerInHouse.InteractAlternate.performed += InteractAlternate_performed;
            playerInputActions.PlayerInHouse.Disable();

        }


    }

    private void Start()
    {
        run = false;
        jump = false;
    }


    private void Update()
    {
        GetAllInputs();
    }


    private void OnEnable()
    {
        

        switch(sceneType) // enable the correct input Map
        {
            case Loader.Scene.GreenHouse:
                ChangeMovement(true);
                playerInput.SwitchCurrentActionMap("PlayerOutSide");
                break;
            case Loader.Scene.DeliveryScene:
                ChangeMovement(true);
                playerInput.SwitchCurrentActionMap("PlayerOutSide");
                break;
            case Loader.Scene.House:
                playerInputActions.PlayerInHouse.Enable();
                playerInput.SwitchCurrentActionMap("PlayerInHouse");
                break;
        }

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
            ChangeMovement(false);
            playerInputActions.PlayerHitMinigame.Enable();
            playerInput.SwitchCurrentActionMap("PlayerHitMinigame");
        }
        else
        {
            playerInputActions.PlayerHitMinigame.Disable();
            ChangeMovement(true);
            playerInput.SwitchCurrentActionMap("PlayerOutSide");
        }
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

    public Vector2 GetMovementVectorNormalizedInHouse()
    {
        Vector2 inputVector = playerInputActions.PlayerInHouse.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        ////arredonda o valor para 0 se ele for menor que 0.4 (isso melhora o movimento pelo controle)
        if ((inputVector.x < 0.4f && inputVector.x > 0f) || (inputVector.x > -0.4f && inputVector.x < 0f))
            inputVector.x = 0f;
        if ((inputVector.y < 0.4f && inputVector.y > 0f) || (inputVector.y > -0.4f && inputVector.y < 0f))
            inputVector.y = 0f;
        return inputVector;
    }

    public void ChangeMovement(bool _enable)
    {
        if(_enable)
            playerInputActions.PlayerOutSide.Enable();
        else
            playerInputActions.PlayerOutSide.Disable();
    }
    public float GetVerticalInput() { return verticalInput; }
    public float GetHorizontalInput() { return horizontalInput; }

    public bool GetRunInput() { return run;  }
    public bool GetJumpInput() { return jump; }
    public void GetAllInputs() => HandleAllInputs();


    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.PlayerOutSide.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.PlayerOutSide.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.PlayerOutSide.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.PlayerOutSide.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.PlayerOutSide.Interact.bindings[0].ToDisplayString();
            case Binding.Jump:
                return playerInputActions.PlayerOutSide.Jump.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        playerInputActions.PlayerOutSide.Disable();
        InputAction inputAction;
        int bindingIndex;

        switch(binding)
        {
            default :
            case Binding.Move_Up:
                inputAction = playerInputActions.PlayerOutSide.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.PlayerOutSide.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.PlayerOutSide.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.PlayerOutSide.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.PlayerOutSide.Interact;
                bindingIndex = 0;
                break;
            case Binding.Jump:
                inputAction = playerInputActions.PlayerOutSide.Jump;
                bindingIndex = 0;
                break;
        }

        //playerInputActions.PlayerOutSide.Move.PerformInteractiveRebinding(1)
        //    .OnComplete(callback => {
        //        callback.Dispose();
        //        playerInputActions.PlayerOutSide.Enable();
        //        OnActionRebound();
        //    })
        //.Start();

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.PlayerOutSide.Enable();
                OnActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
        .Start();
    }

    private void ChangeBind(InputAction inputAction, int bindingIndex)
    {

    }
}
