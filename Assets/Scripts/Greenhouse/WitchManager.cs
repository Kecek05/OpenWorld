using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchManager : MonoBehaviour
{
    private void Update()
    {
        WitchInputs.Instance.GetAllInputs();
    }
    private void LateUpdate()
    {
        WitchMovement.Instance.GetAllMoves();
    }
}
