
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

    private PotionObjectSO potionToDeliveryHere;

    public void Interact()
    {
        //Delivery the potion
        StoredPotions.Instance.DeliveryPotion(potionToDeliveryHere);

        DeliveryMinigame.Instance.StartMinigame(potionToDeliveryHere.potionMoneyRecieve, potionToDeliveryHere.minigameDifficulty);

        gameObject.SetActive(false);
    }

    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;

        OnPotionSet?.Invoke(this, new OnPotionSetEventArgs { potionSprite = potionToDeliveryHere.potionSprite });
    }
}
