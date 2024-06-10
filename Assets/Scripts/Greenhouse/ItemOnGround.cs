using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOnGround : MonoBehaviour, IInteractable, IHasProgress
{

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;
    private ItemOnGroundSO selectedItemOnGroundSO;
    private Transform itemInGround;
    [SerializeField] private Image itemInGroundImage;
    private int selectedItemClicksToCollect;
    [SerializeField] private WitchInventorySO witchInventorySO;
    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;
    [SerializeField] private ParticleSystem spawnParticle;

    private void Start()
    {
        int randomItemOnGround = UnityEngine.Random.Range(0, itemOnGroundSOArray.Length);
        selectedItemOnGroundSO = itemOnGroundSOArray[randomItemOnGround]; 
        selectedItemClicksToCollect = selectedItemOnGroundSO.clicksToCollect;
        itemInGround = Instantiate(selectedItemOnGroundSO.prefab, transform);
        itemInGroundImage.sprite = selectedItemOnGroundSO.itemSprite;
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
                spawnParticle.Play();

                Destroy(itemInGround.gameObject);
                Destroy(gameObject);
                Destroy(itemInGroundImage.gameObject);
                
            }
        }
    }
}
