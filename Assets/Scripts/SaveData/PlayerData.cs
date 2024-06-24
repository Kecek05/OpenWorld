
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int economyPlayer;
    public int dayCount;
    public float[] position;
    public int expansesCount;

    public PlayerData(PaymentController controller)
    {
        economyPlayer = PaymentController.Instance.GetTotalEconomy();
        dayCount = PaymentController.Instance.GetDayCounts();
        expansesCount = PaymentController.Instance.GetExpansesCount();
        Debug.Log("Economy Player is " + economyPlayer);
    }
    
}
