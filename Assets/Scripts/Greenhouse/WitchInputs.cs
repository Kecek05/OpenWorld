using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchInputs : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";


    public static WitchInputs Instance { get; private set; }

    public event EventHandler OnPausePerformed;
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

    public enum Binding
    {
        Move_Up, 
        Move_Down, 
        Move_Left, 
        Move_Right,
        Jump,
        Run,
        Interact,
        Alternate_Interact,
        Pause,
        JumpGamepad,
        RunGamepad,
        InteractGamepad,
        Alternate_InteractGamepad,
        PauseGamepad,
    }

    private Loader.Scene currentScene;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }



            //OutSide
            playerInputActions.PlayerOutSide.Pause.performed += Pause_performed; ;
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
            playerInputActions.PlayerInHouse.Pause.performed += Pause_performed1; ;
            playerInputActions.PlayerInHouse.Move.performed += OnMovementPerformed;
            playerInputActions.PlayerInHouse.Move.canceled += OnMovementCanceled;
            playerInputActions.PlayerInHouse.Run.performed += i => run = true;
            playerInputActions.PlayerInHouse.Run.canceled += i => run = false;
            playerInputActions.PlayerInHouse.Interact.performed += Interact_performed;
            playerInputActions.PlayerInHouse.InteractAlternate.performed += InteractAlternate_performed;
            playerInputActions.PlayerInHouse.Disable();

        }

    }

    private void Pause_performed1(InputAction.CallbackContext obj)
    {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }
    private void Start()
    {
        run = false;
        jump = false;
        ChangeActiveMap(Loader.Scene.GreenHouse);
    }


    private void Update()
    {
        GetAllInputs();
    }


    public void ChangeActiveMap(Loader.Scene _scene)
    {
        currentScene = _scene;
        Debug.Log("CURRENT SCENE ACTIVE MAP IS " + currentScene);
        switch (currentScene) // enable the correct input Map
        {
            case Loader.Scene.GreenHouse:
                playerInputActions.PlayerInHouse.Disable();
                ChangeMovement(true);
                playerInput.SwitchCurrentActionMap("PlayerOutSide");
                break;
            case Loader.Scene.DeliveryScene:
                playerInputActions.PlayerInHouse.Disable();
                ChangeMovement(true);
                playerInput.SwitchCurrentActionMap("PlayerOutSide");
                break;
            case Loader.Scene.House:
                ChangeMovement(false);
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
        switch (binding)
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
            case Binding.Alternate_Interact:
                return playerInputActions.PlayerInHouse.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Jump:
                return playerInputActions.PlayerOutSide.Jump.bindings[0].ToDisplayString();
            case Binding.Run:
                return playerInputActions.PlayerOutSide.Run.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.PlayerOutSide.Pause.bindings[0].ToDisplayString();
            case Binding.JumpGamepad:
                return playerInputActions.PlayerOutSide.Jump.bindings[1].ToDisplayString();
            case Binding.RunGamepad:
                return playerInputActions.PlayerOutSide.Run.bindings[1].ToDisplayString();
            case Binding.InteractGamepad:
                return playerInputActions.PlayerOutSide.Interact.bindings[1].ToDisplayString();
            case Binding.Alternate_InteractGamepad:
                return playerInputActions.PlayerInHouse.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.PauseGamepad:
                return playerInputActions.PlayerOutSide.Pause.bindings[1].ToDisplayString();
        }

    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        playerInputActions.PlayerOutSide.Disable();
        playerInputActions.PlayerInHouse.Disable();
        playerInputActions.PlayerHitMinigame.Disable();

        switch(binding)
        {
            default :
            case Binding.Move_Up:
                ChangeBind(playerInputActions.PlayerOutSide.Move, 1, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Move, 1, OnActionRebound);
                break;
            case Binding.Move_Down:
                ChangeBind(playerInputActions.PlayerOutSide.Move, 2, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Move, 2, OnActionRebound);
                break;
            case Binding.Move_Left:
                ChangeBind(playerInputActions.PlayerOutSide.Move, 3, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Move, 3, OnActionRebound);
                break;
            case Binding.Move_Right:
                ChangeBind(playerInputActions.PlayerOutSide.Move, 4, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Move, 4, OnActionRebound);
                break;
            case Binding.Interact:
                ChangeBind(playerInputActions.PlayerOutSide.Interact, 0, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Interact, 0, OnActionRebound);
                break;
            case Binding.Alternate_Interact:
                ChangeBind(playerInputActions.PlayerInHouse.InteractAlternate, 0, OnActionRebound);
                break;
            case Binding.Jump:
                ChangeBind(playerInputActions.PlayerOutSide.Jump, 0, OnActionRebound);
                break;
            case Binding.Run:
                ChangeBind(playerInputActions.PlayerOutSide.Run, 0, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Run, 0, OnActionRebound);
                break;
            case Binding.Pause:
                ChangeBind(playerInputActions.PlayerOutSide.Pause, 0, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Pause, 0, OnActionRebound);
                ChangeBind(playerInputActions.PlayerHitMinigame.Pause, 0, OnActionRebound);
                break;
            case Binding.JumpGamepad:
                ChangeBind(playerInputActions.PlayerOutSide.Jump, 1, OnActionRebound);
                break;
            case Binding.RunGamepad:
                ChangeBind(playerInputActions.PlayerOutSide.Run, 1, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Run, 1, OnActionRebound);
                break;
            case Binding.InteractGamepad:
                ChangeBind(playerInputActions.PlayerOutSide.Interact, 1, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Interact, 1, OnActionRebound);
                break;
            case Binding.Alternate_InteractGamepad:
                ChangeBind(playerInputActions.PlayerInHouse.InteractAlternate, 1, OnActionRebound);
                break;
            case Binding.PauseGamepad:
                ChangeBind(playerInputActions.PlayerOutSide.Pause, 1, OnActionRebound);
                ChangeBind(playerInputActions.PlayerInHouse.Pause, 1, OnActionRebound);
                ChangeBind(playerInputActions.PlayerHitMinigame.Pause, 1, OnActionRebound);
                break;
        }
        //Finished Rebinding
        EnableCorrectInputAction();
    }

    private void ChangeBind(InputAction inputAction, int bindingIndex, Action OnActionRebound)
    {

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();

                OnActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
        .Start();
    }


    private void EnableCorrectInputAction()
    {
        switch(currentScene)
        {
            case Loader.Scene.House:
                playerInputActions.PlayerInHouse.Enable();
            break;
            case Loader.Scene.GreenHouse:
                playerInputActions.PlayerOutSide.Enable();
            break;
            case Loader.Scene.DeliveryScene:
                playerInputActions.PlayerOutSide.Enable();
            break;
        }
    }
}
