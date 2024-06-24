
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySceneController : MonoBehaviour
{
    //Serialize for now
    public static DeliverySceneController instance;

    [SerializeField] private List<GameObject> deliverySpotsList = new List<GameObject>();

    [SerializeField] private float delayBetweenOrders;

    private IEnumerator creaRandomNewOrderCoroutine;


    private void Start()
    {

        creaRandomNewOrderCoroutine = CreateRandomNewOrder();
        StartCoroutine(creaRandomNewOrderCoroutine);
    }

    private IEnumerator CreateRandomNewOrder()
    {

        while (StoredPotions.Instance.potionsMade.Count > 0)
        {
            //There is a spot to delivery potion

            int randomPotion = Random.Range(0, StoredPotions.Instance.potionsMade.Count); //random recipe
            int randomSpot = Random.Range(0, deliverySpotsList.Count); //random spot

            PotionObjectSO selectedPotion = StoredPotions.Instance.potionsMade[randomPotion];
            GameObject selectedSpot = deliverySpotsList[randomSpot];
            selectedSpot.SetActive(true);

            selectedSpot.GetComponent<DeliverySpot>().SetPotionToDeliveryHere(selectedPotion);

            StoredPotions.Instance.potionsMade.Remove(selectedPotion);
            deliverySpotsList.Remove(selectedSpot);
            yield return new WaitForSeconds(delayBetweenOrders);
        }


    }
}
