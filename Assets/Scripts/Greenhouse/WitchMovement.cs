using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public static WitchMovement main1;

    Vector3 moveDirection;
    Transform cameraObj;
    Rigidbody rb;
    Animator anim;

    private float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpForce;
    private float maxStamina = 4;
    private float currentStamina;

    private bool isGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private Vector3 groundCheckPosition;

    [SerializeField] private AnimationCurve jumpForceCurve;

    private bool isJumping = false;
    private float jumpingTime;
    [SerializeField] private float jumpingMaxTime;
    private IEnumerator jumpingCoroutine;
    
    [SerializeField] private AnimationCurve runSpeedCurve;
    private IEnumerator runCoroutine;
    private float runTime;

    private void Awake()
    {
        main1 = this;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cameraObj = Camera.main.transform;
    }

    private void LateUpdate()
    {
        var groundcheck = Physics.OverlapSphere(transform.position + groundCheckPosition,groundCheckSize, groundLayer);

        if(groundcheck.Length != 0)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
            
        }

        if(isGround == true && WitchInputs.main.GetJumpInput() == true && isJumping == false)
        {
            if(jumpingCoroutine == null)
            {
                isJumping = true;
                jumpingCoroutine = JumpCouroutine();
                StartCoroutine(jumpingCoroutine);
            }
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
        float horizontalInput = WitchInputs.main.GetHorizontalInput();
        float verticalInput = WitchInputs.main.GetVerticalInput();

        Vector3 moveDirection = cameraObj.forward * verticalInput + cameraObj.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        
        if (WitchInputs.main.GetRunInput() == true && currentStamina > 0)
        {
            float evaluatedSpeed = runSpeedCurve.Evaluate(runTime);
            runTime += Time.deltaTime;
            runSpeed = evaluatedSpeed;
            moveDirection = moveDirection * runSpeed;
            currentStamina -= Time.deltaTime;

        }
        else if (WitchInputs.main.GetRunInput() == true && currentStamina <= 0)
        {
            runTime = 0;
            moveDirection = moveDirection * moveSpeed;
        }
        else
        {
            runTime = 0;
            moveDirection = moveDirection * moveSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina += Time.deltaTime;
            }
        }


        Vector3 movementVelocity = moveDirection;
        movementVelocity.y = rb.velocity.y;
        rb.velocity = movementVelocity;
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
            yield return null;
        }
        isJumping = false;
        jumpingCoroutine = null;
    }

    private void HandleAirMovement()
    {
        float horizontalInput = WitchInputs.main.GetHorizontalInput();
        float verticalInput = WitchInputs.main.GetVerticalInput();

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
        targetDirection = cameraObj.forward * WitchInputs.main.GetVerticalInput();
        targetDirection = targetDirection + cameraObj.right * WitchInputs.main.GetHorizontalInput();
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void GetAllMoves() => HandleAllMovement();

}
