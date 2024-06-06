
using System;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySpot : MonoBehaviour, IInteractable
{
    public event EventHandler<OnPotionSetEventArgs> OnPotionSet;

    public class OnPotionSetEventArgs
    {
        public Sprite potionSprite;
    }

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

        OnPotionSet?.Invoke(this, new OnPotionSetEventArgs { potionSprite = potionToDeliveryHere.potionSprite });
    }
}
