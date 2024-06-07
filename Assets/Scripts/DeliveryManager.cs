using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSuccess; // for SFX
    public event EventHandler OnRecipeFailed; // for SFX

    public event EventHandler<OnRecipeCompletedEventArgs> OnRecipeCompleted;

    public class OnRecipeCompletedEventArgs
    {
        public PotionObjectSO completedPotion;
    }


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    [SerializeField] private StoredPotionsSO storedPotionsSO;

    private void Awake()
    {
        Instance = this;

    }



    public void DeliverRecipe(PlateKitchenObject deliveredPlateKitchenObject, GameObject potionShapeObject)
    {
        for (int i = 0; i < recipeListSO.potionObjectSOList.Count; ++i)
        {
            PotionObjectSO waitingRecipeSO = recipeListSO.potionObjectSOList[i];
            if (waitingRecipeSO.ingredientsSOList.Count == deliveredPlateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipekitchenObjectSO in waitingRecipeSO.ingredientsSOList)
                {
                    //Cycling through all ingredients in the recipe
                    bool ingredientFount = false;
                    foreach (KitchenObjectSO platekitchenObjectSO in deliveredPlateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients in the plate
                        if (platekitchenObjectSO == recipekitchenObjectSO)
                        {
                            //Ingredient matches!
                            ingredientFount = true;
                            break;
                        }
                    }
                    if (!ingredientFount)
                    {
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //player delivered the correct ingredients!
                    if(potionShapeObject.CompareTag(waitingRecipeSO.PotionShape.tag))
                    {
                        //same potion shape
                        storedPotionsSO.StorePotion(deliveredPlateKitchenObject.GetPotionObjectSOInThisPlate());

                        OnRecipeCompleted?.Invoke(this, new OnRecipeCompletedEventArgs
                        {
                            completedPotion = deliveredPlateKitchenObject.GetPotionObjectSOInThisPlate()
                        });
                        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                        return;
                    } else
                    {
                        //wrong potion shape
                    }
                }
            }
        }
        //No matches found!
        // Player did not delivered a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
}
