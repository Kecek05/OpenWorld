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
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxStamina = 4;
    [SerializeField] private float currentStamina;

    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private Vector3 groundCheckPosition;

    [SerializeField] private AnimationCurve jumpForceCurve;

    private bool isJumping = false;
    private float jumpingTime;
    [SerializeField] private float jumpingMaxTime;
    private IEnumerator jumpingCoroutine;


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
        //anim.SetBool("Jump", !isGround); colocar a animação de pulo aqui

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



    private IEnumerator JumpCouroutine()
    {
        jumpForce = jumpForceCurve.Evaluate(0);
        jumpingTime = 0f;
        while(jumpingTime < jumpingMaxTime)
        {
            jumpingTime += Time.deltaTime;
            jumpForce = jumpForceCurve.Evaluate(jumpingTime);
            rb.velocity = transform.up * jumpForce;
            //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            yield return null;
        }
        isJumping = false;
        jumpingCoroutine = null;
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
            moveDirection = moveDirection * runSpeed;
            currentStamina -= Time.deltaTime;
        }
        else if(WitchInputs.main.GetRunInput() == true && currentStamina <= 0)
        {
            moveDirection = moveDirection * moveSpeed;
        }
        else
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCheckPosition, groundCheckSize);
    }
    public void GetAllMoves() => HandleAllMovement();

}
