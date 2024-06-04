using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSpawned;
    public event EventHandler<OnRecipeCompletedEventArgs> OnRecipeCompleted;

    public class OnRecipeCompletedEventArgs
    {
        public PotionObjectSO completedPotion;
    }


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<PotionObjectSO> recipesPotionObjectSOList;


    private void Awake()
    {
        Instance = this;
        recipesPotionObjectSOList = new List<PotionObjectSO>();
    }

    private void Update()
    {
        //spawnRecipeTimer -= Time.deltaTime;
        //if(spawnRecipeTimer <= 0f)
        //{
        //    spawnRecipeTimer = spawnRecipeTimerMax;
        //    if(waitingPotionObjectSOList.Count < waitingRecipeMax)
        //    {
        //        PotionObjectSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

        //        waitingPotionObjectSOList.Add(waitingRecipeSO);

        //        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        //    }
        //}
    }

    private void Start()
    {

        for(int i = 0; i  < recipeListSO.recipeSOList.Count; i++)
        {
            PotionObjectSO potionSelected = recipeListSO.recipeSOList[i];

            recipesPotionObjectSOList.Add(potionSelected);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }



    public void DeliverRecipe(PlateKitchenObject deliveredPlateKitchenObject, GameObject potionShapeObject)
    {
        for (int i = 0; i < recipesPotionObjectSOList.Count; ++i)
        {
            PotionObjectSO waitingRecipeSO = recipesPotionObjectSOList[i];
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
                        StoredPotionsController.Instance.StorePotion(deliveredPlateKitchenObject.GetPotionObjectSOInThisPlate());

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


    public List<PotionObjectSO> GetRecipeListPotionObjectSOList()
    {
        return recipeListSO.recipeSOList;
    }
}
