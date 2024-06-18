using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int economyPlayer;
    public int dayCount;
    public float[] position;

    public PlayerData(PaymentController controller)
    {
        economyPlayer = PaymentController.Instance.GetTotalEconomy();
        dayCount = PaymentController.Instance.GetDayCounts();
    }
    
}
