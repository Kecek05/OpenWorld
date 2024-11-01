using System.Collections;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    private Vector3 moveDirection;
    private Transform cameraObj;
    private Rigidbody rb;

    private float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpForce;

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
    private float runTime;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
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

        if(isGround == true && WitchInputs.Instance.GetJumpInput() == true && isJumping == false)
        {
            if(jumpingCoroutine == null)
            {
                isJumping = true;
                jumpingCoroutine = JumpCouroutine();
                StartCoroutine(jumpingCoroutine);
            }
        }
        HandleAllMovement();
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

        
        if (WitchInputs.Instance.GetRunInput() == true)
        {
            float evaluatedSpeed = runSpeedCurve.Evaluate(runTime);
            runTime += Time.deltaTime;
            runSpeed = evaluatedSpeed;
            moveDirection = moveDirection * runSpeed;

        }
        else if (WitchInputs.Instance.GetRunInput() == true)
        {
            runTime = 0;
            moveDirection = moveDirection * moveSpeed;
        }
        else
        {
            runTime = 0;
            moveDirection = moveDirection * moveSpeed;
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

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

}
