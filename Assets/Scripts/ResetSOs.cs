using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSOs : MonoBehaviour
{
    [SerializeField] private StoredPotions storedPotionsSO;
    [SerializeField] private WitchInventorySO witchInventorySO;
    [SerializeField] private PlayerItems playerItemsSO;
    private void Start()
    {
        //Reset the SOs
        //playerItemsSO.ResetPlayerItems();
        //witchInventorySO.ResetInventoryList();
        //storedPotionsSO.ResetPotionsMade();
    }
}
