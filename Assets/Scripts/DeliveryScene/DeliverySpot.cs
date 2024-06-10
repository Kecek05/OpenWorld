
using System;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject parentMinigame;

    public event EventHandler<OnPotionSetEventArgs> OnPotionSet;

    public class OnPotionSetEventArgs
    {
        public Sprite potionSprite;
    }

    private PotionObjectSO potionToDeliveryHere;

    public void Interact()
    {
        StoredPotions.Instance.DeliveryPotion(potionToDeliveryHere);

        MoneyController.Instance.SetCurrentMoney(MoneyController.Instance.GetCurrentMoney() + potionToDeliveryHere.potionMoneyRecieve);

        parentMinigame.SetActive(true);

        gameObject.SetActive(false);
    }

    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;

        OnPotionSet?.Invoke(this, new OnPotionSetEventArgs { potionSprite = potionToDeliveryHere.potionSprite });
    }
}
