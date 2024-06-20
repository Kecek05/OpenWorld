using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOnGround : MonoBehaviour, IInteractable, IHasProgress
{

    public event Action OnItemClicked;
    public event Action OnItemCollected;

    [SerializeField] private ItemOnGroundSO[] itemOnGroundSOArray;
    private Transform itemInGround;

    [SerializeField] private Transform spawnpoint;

    private ItemOnGroundSO selectedItemOnGroundSO;


    [SerializeField] private Image itemInGroundImage;
    [SerializeField] private Image ItemInGroundMinimap;

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
        itemInGround.GetComponent<SelectedIngredient>().SetParent(this.gameObject);
        itemInGroundImage.sprite = selectedItemOnGroundSO.itemSprite;
        ItemInGroundMinimap.sprite = selectedItemOnGroundSO.itemSprite;
    }


    public void Interact()
    {
        if (witchInventorySO.CanAddMoreItens())
        {
            //Inv not full
            selectedItemClicksToCollect--;
            OnItemClicked?.Invoke(); //SFX

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
                Debug.Log("Coletado");
                OnItemCollected?.Invoke(); // SFX
                Destroy(itemInGround.gameObject);
                //selectedItemOnGroundSO = null;
                Destroy(gameObject);
                Destroy(itemInGroundImage.gameObject);
                
            }
        }
    }
}
