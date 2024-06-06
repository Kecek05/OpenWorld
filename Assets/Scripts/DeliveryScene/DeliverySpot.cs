
using UnityEngine;

public class DeliverySpot : MonoBehaviour
{

    private PotionObjectSO potionToDeliveryHere;


    public void SetPotionToDeliveryHere(PotionObjectSO _potionToDelivery)
    {
        //Set the potion here
        potionToDeliveryHere = _potionToDelivery;
        print(potionToDeliveryHere);
    }
}
