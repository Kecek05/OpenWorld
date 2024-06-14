using System;

using UnityEngine;


public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    public event EventHandler OnChangedItemCount;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private PlayerItems.ItensType CounterType;


    public override void Interact(PlayerInHouse player)
    {
        if(!player.HasKitchenObject() && PlayerItems.Instance.TrySpawnItem(CounterType))
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
                PlayerItems.Instance.ReturnItemToCountainer(CounterType);


                //Destroy the item

                player.GetKitchenObject().DestroySelf();
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                OnChangedItemCount?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public PlayerItems.ItensType GetCounterType() { return CounterType; }
}
