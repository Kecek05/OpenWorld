using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchManager : MonoBehaviour
{
    private void Update()
    {
        WitchInputs.main.GetAllInputs();
        
    }
    private void LateUpdate()
    {
        WitchMovement.main1.GetAllMoves();
    }
}
