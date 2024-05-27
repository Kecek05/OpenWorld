using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            playerInputActions.PlayerMovement.Move.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
    
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    public float GetVerticalInput() {  return verticalInput; }


}
