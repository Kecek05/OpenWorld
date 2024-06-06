
using UnityEngine;

public class DeliverySpot : MonoBehaviour, IInteractable
{
    [SerializeField] private StoredPotionsSO storedPotionsSO;

    private PotionObjectSO potionToDeliveryHere;

    public void Interact()
    {
        storedPotionsSO.DeliveryPotion(potionToDeliveryHere);

        MoneyController.Instance.SetCurrentMoney(MoneyController.Instance.GetCurrentMoney() + potionToDeliveryHere.potionMoneyRecieve);
        gameObject.SetActive(false);
    }

    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;
    }
}
