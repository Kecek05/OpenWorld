
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySceneController : MonoBehaviour
{
    //Serialize for now

    [SerializeField] private List<GameObject> deliverySpotsList = new List<GameObject>();

    [SerializeField] private float delayBetweenOrders;

    private IEnumerator creaRandomNewOrderCoroutine;

    [SerializeField] private StoredPotionsSO storedPotionsSO;

    private void Awake()
    {
        storedPotionsSO.ResetPotionsMade();
    }

    private void Start()
    {

        creaRandomNewOrderCoroutine = CreateRandomNewOrder();
        StartCoroutine(creaRandomNewOrderCoroutine);
    }

    private IEnumerator CreateRandomNewOrder()
    {
        while (storedPotionsSO.potionsMade.Count > 0)
        {
            //There is a spot to delivery potion

            int randomPotion = Random.Range(0, storedPotionsSO.potionsMade.Count); //random recipe
            int randomSpot = Random.Range(0, deliverySpotsList.Count); //random spot

            PotionObjectSO selectedPotion = storedPotionsSO.potionsMade[randomPotion];
            GameObject selectedSpot = deliverySpotsList[randomSpot];
            selectedSpot.SetActive(true);

            selectedSpot.GetComponent<DeliverySpot>().SetPotionToDeliveryHere(selectedPotion);

            storedPotionsSO.potionsMade.Remove(selectedPotion);
            deliverySpotsList.Remove(selectedSpot);
            yield return new WaitForSeconds(delayBetweenOrders);
        }


    }
}
