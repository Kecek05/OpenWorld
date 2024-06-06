
using UnityEngine;

public class DeliverySpot : MonoBehaviour, IInteractable
{

    private PotionObjectSO potionToDeliveryHere;

    public void Interact()
    {
        MoneyController.Instance.SetCurrentMoney(MoneyController.Instance.GetCurrentMoney() + potionToDeliveryHere.potionMoneyRecieve);

        Debug.Log("Delivered, money earned is " + potionToDeliveryHere.potionMoneyRecieve);
        gameObject.SetActive(false);
    }

    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;
        print(potionToDeliveryHere);
    }
}
