using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeWrong;

    public event EventHandler<OnRecipeCompletedEventArgs> OnRecipeCompleted;

    public class OnRecipeCompletedEventArgs
    {
        public PotionObjectSO completedPotion;
    }

    private int correctRecipeCount = 0;


    private void Awake()
    {
        Instance = this;

    }



    public void DeliverRecipe(PlateKitchenObject deliveredPlateKitchenObject, GameObject potionShapeObject)
    {
        for (int i = 0; i < RandomizeRecipeController.Instance.GetSelectedPotionsSOList().Count; ++i)
        {
            PotionObjectSO waitingRecipeSO = RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i];
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
                        correctRecipeCount++;
                        //same potion shape
                        StoredPotions.Instance.StorePotion(deliveredPlateKitchenObject.GetPotionObjectSOInThisPlate());

                        OnRecipeCompleted?.Invoke(this, new OnRecipeCompletedEventArgs
                        {
                            completedPotion = deliveredPlateKitchenObject.GetPotionObjectSOInThisPlate()
                        });
                        return;
                    } else
                    {
                        //wrong potion shape
                        OnRecipeWrong?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
        //No matches found!
        // Player did not delivered a correct recipe
        OnRecipeWrong?.Invoke(this, EventArgs.Empty);
    }

    public int GetCorrectRecipeCount() { return correctRecipeCount; }
}
