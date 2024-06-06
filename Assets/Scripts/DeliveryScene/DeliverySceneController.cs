
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySceneController : MonoBehaviour
{
    //Serialize for now
    [SerializeField] private List<PotionObjectSO> madePotionList = new List<PotionObjectSO>();

    [SerializeField] private List<GameObject> deliverySpotsList = new List<GameObject>();


    [SerializeField] private float delayBetweenOrders;

    //temp
    private bool inGame = true;

    private IEnumerator creaRandomNewOrderCoroutine;

    private void Start()
    {
        creaRandomNewOrderCoroutine = CreateRandomNewOrder();
        StartCoroutine(creaRandomNewOrderCoroutine);
    }


    private IEnumerator CreateRandomNewOrder()
    {
        while (inGame)
        {
            int randomPotion = Random.Range(0, madePotionList.Count); //random recipe
            int randomSpot = Random.Range(0, deliverySpotsList.Count); //random spot

            GameObject selectedSpot = deliverySpotsList[randomSpot];
            selectedSpot.SetActive(true);

            selectedSpot.GetComponent<DeliverySpot>().SetPotionToDeliveryHere(madePotionList[randomPotion]);
            deliverySpotsList.Remove(selectedSpot);

            yield return new WaitForSeconds(delayBetweenOrders);
        }


    }
}
