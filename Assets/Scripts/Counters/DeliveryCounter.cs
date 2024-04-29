using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) // ele tenta ver se o player possui um prato na mao, e se for um prato ele retorna o prato e o valor boleano na variavel PlateKitchenObject
            {
              player.GetKitchenObject().DestroySelf();

            }
        }
    }




}
