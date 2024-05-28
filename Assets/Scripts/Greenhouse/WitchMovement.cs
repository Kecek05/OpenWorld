using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public static WitchMovement main1;

    Vector3 moveDirection;
    Transform cameraObj;
    Rigidbody rb;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 15f;

    private void Awake()
    {
        main1 = this;
        rb = GetComponent<Rigidbody>();
        cameraObj = Camera.main.transform;
    }

  

    private void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObj.forward * WitchInputs.main.GetVerticalInput();
        moveDirection = moveDirection + cameraObj.right * WitchInputs.main.GetHorizontalInput();
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed;

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
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
