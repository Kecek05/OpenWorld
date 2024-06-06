using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour, IInteractable, IHasProgress
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;
    private ItemOnGroundSO selectedItemOnGroundSO;
    private Transform itemInGround;
    private int selectedItemClicksToCollect;

    [SerializeField] private WitchInventorySO witchInventorySO;

    private int collectProgress;

    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    private void Start()
    {
        int randomItemOnGround = UnityEngine.Random.Range(0, itemOnGroundSOArray.Length);
        selectedItemOnGroundSO = itemOnGroundSOArray[randomItemOnGround]; 
        selectedItemClicksToCollect = selectedItemOnGroundSO.clicksToCollect;
        itemInGround = Instantiate(selectedItemOnGroundSO.prefab, transform.position, Quaternion.identity);
    }


    public void Interact()
    {
        if (witchInventorySO.CanAddMoreItens())
        {
            selectedItemClicksToCollect--;

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
            {
                progressNormalized = 1f - (float)selectedItemClicksToCollect / selectedItemOnGroundSO.clicksToCollect, 
                progressCount = selectedItemClicksToCollect
            });

            if (selectedItemClicksToCollect <= 0)
            {
                witchInventorySO.AddItemToInventoryList(selectedItemOnGroundSO);
                Destroy(itemInGround.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
