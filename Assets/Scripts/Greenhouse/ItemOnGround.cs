using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOnGround : MonoBehaviour, IInteractable, IHasProgress
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;
    private Transform itemInGround;

    [SerializeField] private Transform spawnpoint;

    private ItemOnGroundSO selectedItemOnGroundSO;


    [SerializeField] private Image itemInGroundImage;

    private int selectedItemClicksToCollect; // clicks need to collect

    [SerializeField] private WitchInventorySO witchInventorySO;

    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    [SerializeField] private ParticleSystem spawnParticle;

    private void Start()
    {
        int randomItemOnGround = UnityEngine.Random.Range(0, itemOnGroundSOArray.Length);
        selectedItemOnGroundSO = itemOnGroundSOArray[randomItemOnGround]; 
        selectedItemClicksToCollect = selectedItemOnGroundSO.clicksToCollect;
        itemInGround = Instantiate(selectedItemOnGroundSO.prefab, spawnpoint.transform);
        itemInGroundImage.sprite = selectedItemOnGroundSO.itemSprite;
    }


    public void Interact()
    {
        if (witchInventorySO.CanAddMoreItens())
        {
            //Inv not full
            selectedItemClicksToCollect--; 

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
            {
                progressNormalized = 1f - (float)selectedItemClicksToCollect / selectedItemOnGroundSO.clicksToCollect, 
                progressCount = selectedItemClicksToCollect
            });

            if (selectedItemClicksToCollect <= 0)
            {
                //done all clicks

                //add item
                witchInventorySO.AddItemToInventoryList(selectedItemOnGroundSO);

                
                spawnParticle.Play();

                Destroy(itemInGround.gameObject);
                //selectedItemOnGroundSO = null;
                Destroy(gameObject);
                Destroy(itemInGroundImage.gameObject);
                
            }
        }
    }
}
