using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer Instance;

    public static event Action OnPlayerFalling;
    public static event Action OnPlayerJumping;
    public static event Action OnPlayerIdle;
    public static event Action OnPlayerWalking; 
    public static event Action OnPlayerRunning; 

    public event Action<GameObject> OnInteractObjectChanged;

    protected GameObject intectableObj;

    //Movement
    private Vector3 moveDirection;
    private Transform cameraObj;
    private Rigidbody rb;

    private float rotationSpeed = 10f;
    [Space]
    [Header("Walk Stats")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float runSpeed = 12f;
    private float jumpForce;
    [Space]
    private bool isGround;
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private Vector3 groundCheckPosition;
    [Space]
    [Header("Jump Stats")]
    [SerializeField] private float jumpingMaxTime;
    [Space]
    [Header("Curves")]
    [SerializeField] private AnimationCurve jumpForceCurve;
    [SerializeField] private AnimationCurve runSpeedCurve;
    [Space]

    private bool isJumping = false;
    private float jumpingTime;
    private IEnumerator jumpingCoroutine;

    private float runTime;

    private enum PlayerState
    {
        IDLE,
        WALK,
        RUN,
        INTERACT,
        JUMP,
        FALL,
    }
    private PlayerState playerState;

    protected virtual void Awake()
    {
        Instance = this;
        cameraObj = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        WitchInputs.Instance.OnInteractAction += WitchInputs_OnInteractAction;
        WitchInputs.Instance.OnInteractAlternateAction += WitchInputs_OnInteractAlternateAction; ;
    }

    protected void LateUpdate()
    {
        var groundcheck = Physics.OverlapSphere(transform.position + groundCheckPosition, groundCheckSize, groundLayer);

        if (groundcheck.Length != 0)
        {
            isGround = true;
        }
        else
        {
            isGround = false;

        }

        if (isGround == true && WitchInputs.Instance.GetJumpInput() == true && isJumping == false)
        {
            if (jumpingCoroutine == null)
            {
                ChangePlayerState(PlayerState.JUMP);
                isJumping = true;
                jumpingCoroutine = JumpCouroutine();
                StartCoroutine(jumpingCoroutine);
            }
        }
        HandleAllMovement();
    }

    private void ChangePlayerState(PlayerState newPlayerState)
    {
        if(playerState != newPlayerState)
        {
            //New State
            playerState = newPlayerState;
            switch(playerState)
            {
                case PlayerState.IDLE:
                    OnPlayerIdle?.Invoke();
                    break;
                case PlayerState.WALK:
                    OnPlayerWalking?.Invoke();
                    break;
                case PlayerState.RUN:
                    OnPlayerRunning?.Invoke();
                    break;
                case PlayerState.JUMP:
                    OnPlayerJumping?.Invoke();
                    break;
                case PlayerState.INTERACT:
                    Debug.Log("On Interact");
                    break;
                case PlayerState.FALL:
                    OnPlayerFalling?.Invoke();
                    break;
            }
        }
    }


    protected virtual void WitchInputs_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        Debug.LogWarning("Interact Alternate not implemented");
    }

    protected virtual void WitchInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.LogWarning("Interact not implemented");
    }


    protected void OnTriggerStay(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            intectableObj = other.gameObject;
            OnInteractObjectChanged?.Invoke(other.gameObject);
        }

    }

    protected void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {

            intectableObj = null;
            OnInteractObjectChanged?.Invoke(null);
        }
    }



    private void HandleAllMovement()
    {
        if (isGround)
        {
            HandleMovement();
        }
        else
        {
            HandleAirMovement();
        }
        HandleRotation();
    }

    private void HandleMovement()
    {
        float horizontalInput = WitchInputs.Instance.GetHorizontalInput();
        float verticalInput = WitchInputs.Instance.GetVerticalInput();

        Vector3 moveDirection = cameraObj.forward * verticalInput + cameraObj.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;


        if (WitchInputs.Instance.GetRunInput() == true && isGround) // can only run in ground
        {
            //In ground and pressing run button
            float evaluatedSpeed = runSpeedCurve.Evaluate(runTime);
            runTime += Time.deltaTime;
            runSpeed = evaluatedSpeed;
            moveDirection = moveDirection * runSpeed;


            if (moveDirection != Vector3.zero)
                ChangePlayerState(PlayerState.RUN);
        }
        else
        {
            //Not in ground or not pressing the btn
            runTime = 0;
            moveDirection = moveDirection * moveSpeed;
            if(moveDirection != Vector3.zero && !isJumping)
                ChangePlayerState(PlayerState.WALK);
        }



        Vector3 movementVelocity = moveDirection;
        movementVelocity.y = rb.velocity.y;
        rb.velocity = movementVelocity;

        if(moveDirection == Vector3.zero && !isJumping)
            ChangePlayerState(PlayerState.IDLE);


    }




    private IEnumerator JumpCouroutine()
    {
        jumpForce = jumpForceCurve.Evaluate(0);
        jumpingTime = 0f;
        while (jumpingTime < jumpingMaxTime)
        {
            jumpingTime += Time.deltaTime;
            jumpForce = jumpForceCurve.Evaluate(jumpingTime);
            rb.velocity = transform.up * jumpForce;
            if(jumpingTime >= jumpingMaxTime/2) //  half way the jump, now its falling
                ChangePlayerState(PlayerState.FALL);
            yield return null;
        }
        isJumping = false;
        jumpingCoroutine = null;
    }

    private void HandleAirMovement()
    {
        float horizontalInput = WitchInputs.Instance.GetHorizontalInput();
        float verticalInput = WitchInputs.Instance.GetVerticalInput();

        Vector3 moveDirection = cameraObj.forward * verticalInput + cameraObj.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        moveDirection *= moveSpeed * 1.5f;


        Vector3 airVelocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = airVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObj.forward * WitchInputs.Instance.GetVerticalInput();
        targetDirection = targetDirection + cameraObj.right * WitchInputs.Instance.GetHorizontalInput();
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }



    public GameObject GetInteractableObj() { return intectableObj; }
}
