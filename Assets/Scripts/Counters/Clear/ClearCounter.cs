using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public event Action OnItemMoved;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(PlayerInHouse player)
    {
        if(!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
                OnItemMoved?.Invoke();
            } else
            {
                //player carrying anything
            }
        } else
        {
            // there is kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPotionKitchenObject, out GameObject potionShapeObject))
                {
                    //player is holding a potion
                    if(GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlateKitchenObject, out GameObject counterPotionShapeObject)) {
                        //Potion on the counter
                        PotionObjectSO potionObjectSOInPlayer = playerPotionKitchenObject.GetPotionObjectSOInThisPlate();
                        if(potionObjectSOInPlayer != null)
                        {
                            //its a compleated potion on player hands
                            counterPlateKitchenObject.SetPotionObjectSOInThisPlate(potionObjectSOInPlayer);
                            for (int i = 0; i < 3; i++) {
                                //3 ingredients
                                counterPlateKitchenObject.AddIngredientToPotion(potionObjectSOInPlayer);
                            }
                            //Destroy potion on player hand
                            player.GetKitchenObject().DestroySelf();
                            OnItemMoved?.Invoke();
                        } else
                        {
                            // not compleated potion, empty in player hand
                            PotionObjectSO potionObjectSOInCounter = counterPlateKitchenObject.GetPotionObjectSOInThisPlate();
                            if(potionObjectSOInCounter != null)
                            {
                                //its a compleated potion on counter
                                playerPotionKitchenObject.SetPotionObjectSOInThisPlate(potionObjectSOInCounter);
                                for (int i = 0; i < 3; i++)
                                {
                                    //3 ingredients
                                    playerPotionKitchenObject.AddIngredientToPotion(potionObjectSOInCounter);
                                }
                                //Destroy potion on counter
                                GetKitchenObject().DestroySelf();
                                OnItemMoved?.Invoke();
                            } else
                            {
                                // not a compleated potion on counter and either in player, do nothing
                            }

                        }
                    }

                }

            } else
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                OnItemMoved?.Invoke();
            }
        }

    }
}
