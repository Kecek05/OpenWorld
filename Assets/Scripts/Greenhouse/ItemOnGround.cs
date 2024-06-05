using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;
    private ItemOnGroundSO selectedItemOnGroundSO;
    private Transform itemInGround;
    private int selectedItemClicksToCollect;


    private void Start()
    {
        int randomItemOnGround = Random.Range(0, itemOnGroundSOArray.Length);
        selectedItemOnGroundSO = itemOnGroundSOArray[randomItemOnGround]; 
        selectedItemClicksToCollect = selectedItemOnGroundSO.clicksToCollect;
        itemInGround = Instantiate(selectedItemOnGroundSO.prefab, transform.position, Quaternion.identity);
    }


    public void Interact()
    {
        if (WitchInventory.Instance.CanAddMoreItens())
        {
            selectedItemClicksToCollect--;

            if(selectedItemClicksToCollect <= 0)
            {
                WitchInventory.Instance.AddItemToInventoryList(selectedItemOnGroundSO);
                Destroy(itemInGround.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
