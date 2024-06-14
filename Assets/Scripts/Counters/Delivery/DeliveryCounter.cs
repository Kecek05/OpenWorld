
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(PlayerInHouse player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject, out GameObject potionShapeObject)) 
            {
                // ele tenta ver se o player possui um prato na mao, e se for um prato ele retorna o prato e o valor boleano na variavel PlateKitchenObject
                //only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject, potionShapeObject);
                player.GetKitchenObject().DestroySelf();

            }
        }
    }




}
