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
    public event EventHandler OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<PotionObjectSO> waitingPotionObjectSOList;


    private void Awake()
    {
        Instance = this;
        waitingPotionObjectSOList = new List<PotionObjectSO>();
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

            waitingPotionObjectSOList.Add(potionSelected);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingPotionObjectSOList.Count; ++i)
        {
            PotionObjectSO waitingRecipeSO = waitingPotionObjectSOList[i];
            if(waitingRecipeSO.ingredientsSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipekitchenObjectSO in waitingRecipeSO.ingredientsSOList)
                {
                    //Cycling through all ingredients in the recipe
                    bool ingredientFount = false;
                    foreach (KitchenObjectSO platekitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients in the plate
                        if(platekitchenObjectSO == recipekitchenObjectSO)
                        {
                            //Ingredient matches!
                            ingredientFount = true;
                            break;
                        }
                    }
                    if(!ingredientFount)
                    {
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //player delivered the correct recipe!

                    //waitingPotionObjectSOList.RemoveAt(i);
                    StoredPotionsController.main.StorePotion(plateKitchenObject.GetPotionObjectSOInThisPlate());

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
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
