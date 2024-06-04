using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchManager : MonoBehaviour
{
    private WitchInventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new WitchInventory();
        uiInventory.SetInventory(inventory);
    }
    private void Update()
    {
        WitchInputs.main.GetAllInputs();
        
    }
    private void LateUpdate()
    {
        WitchMovement.main1.GetAllMoves();
    }
}
