
using UnityEngine;

public class DeliverySpot : MonoBehaviour, IInteractable
{

    private PotionObjectSO potionToDeliveryHere;

    public void Interact()
    {
        Debug.Log("Delivered");
        gameObject.SetActive(false);
    }

    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;
        print(potionToDeliveryHere);
    }
}
