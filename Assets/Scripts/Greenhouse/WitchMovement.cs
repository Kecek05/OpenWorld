using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    
    Vector3 moveDirection;

    
    private void HandleMovement()
    {
        moveDirection.y = WitchInputs.main.GetVerticalInput();
       // moveDirection.x = 
    }


}
