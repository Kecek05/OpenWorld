using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private PlayerItens.ItensType CounterType;

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject() && PlayerItens.Instance.TrySpawnItem(CounterType))
        {
            //player is not carrying anything and can grab one more kitchenObject
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        } else if (player.HasKitchenObject())
        {
            //player is carrying something
            if(player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO)
            {
                //player is carrying same kitchenObject of the Countainer

                //Return the item
                PlayerItens.Instance.ReturnItemToCountainer(CounterType);

                //Destroy the item
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
