using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    public event EventHandler OnChangedItemCount;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private PlayerItemsSO.ItensType CounterType;

    [SerializeField] private PlayerItemsSO playerItemsSO;
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject() && playerItemsSO.TrySpawnItem(CounterType))
        {
            //player is not carrying anything and can grab one more kitchenObject
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            OnChangedItemCount?.Invoke(this, EventArgs.Empty);

        } else if (player.HasKitchenObject())
        {
            //player is carrying something
            if(player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO)
            {
                //player is carrying same kitchenObject of the Countainer

                //Return the item
                playerItemsSO.ReturnItemToCountainer(CounterType);


                //Destroy the item
                player.GetKitchenObject().DestroySelf();
                OnChangedItemCount?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public PlayerItemsSO.ItensType GetCounterType() { return CounterType; }
}
