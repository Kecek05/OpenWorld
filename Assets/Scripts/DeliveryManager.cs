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

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; ++i)
        {
           RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipekitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
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

                    waitingRecipeSOList.RemoveAt(i);

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


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
