using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    public event EventHandler<OnStateCHangedEventArgs> OnStateChanged;
    public class OnStateCHangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        CookingIngredient,
        CookedIngredient,
        Done,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRepiceSO[] burningRecipeSOArray;
    [SerializeField] private PotionCaulderonRecipeSO[] potionCaulderonRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private float burningTimer;
    private BurningRepiceSO burningRecipeSO;

    private float cookingTimer;
    private int totalIngredientInCauldron;

    private int waitingToCook;

    private int cookedIngredientCount;

    private PotionCaulderonRecipeSO currentPotionCaulderonRecipe;

    private float cookingTimerIngredient = 5f;


    private List<KitchenObjectSO> kitchenObjectSOInCaulderonList = new List<KitchenObjectSO>();

    private void Start()
    {
        state = State.Idle; 
    }
    private void Update()
    {
        if(HasKitchenObject())
        {
            switch(state)
            {
                case State.Idle:
                    //nothing
                    break;
                case State.CookingIngredient:

                    cookingTimer -= Time.deltaTime;



                    if (cookingTimer <= 0)
                    {
                        //Cooking Done
                        cookedIngredientCount++;
                       
                        state = State.CookedIngredient; 


                    }

                    Debug.Log(cookingTimer);

                    //fryingTimer += Time.deltaTime;

                    //OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    //{
                    //    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    //});

                    //if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    //{
                    //    //Fried
                    //    GetKitchenObject().DestroySelf();

                    //    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                    //    state = State.Fried;
                    //    burningTimer = 0f;
                    //    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    //    OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                    //    {
                    //        state = state
                    //    });
                    //}
                    break;
                case State.CookedIngredient:


                    if(cookedIngredientCount >= 3)
                    {
                        //finished all 3

                        state = State.Done;

                        //cookingTimer = cookingTimerIngredient;

                        //state = State.CookedIngredient;
                        //potion done
                    } else
                    {
                        //can cook more
                        if(waitingToCook >= 1)
                        {
                            //there is one more to cook
                            waitingToCook--;
                            cookingTimer = 5f;
                            state = State.CookingIngredient;
                        }
                    }

                    //burningTimer += Time.deltaTime;

                    //OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    //{
                    //    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    //});

                    //if (burningTimer > burningRecipeSO.burningTimerMax)
                    //{
                    //    //Fried
                    //    GetKitchenObject().DestroySelf();

                    //    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                    //    state = State.Done;
                    //    OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                    //    {
                    //        state = state
                    //    });

                    //    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    //    {
                    //        progressNormalized = 0f
                    //    });
                    //}
                    break;
                case State.Done:
                    break;

            }
        }
        Debug.Log("State " + state);
    }



    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (GetExistRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carrying something that can go to the caulderon
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    //fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    //fryingTimer = 0f;


                    cookingTimer += 5f;
                    totalIngredientInCauldron = 0;
                    totalIngredientInCauldron++;

                    //kitchenObjectSOInCaulderonList.Clear(); // reset the list
                    kitchenObjectSOInCaulderonList.Add(GetKitchenObject().GetKitchenObjectSO());


                    state = State.CookingIngredient;

                    OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });



                   
                }
            }
            else
            {
                //player carrying anything
            }
        }
        else
        {
            // there is kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something

                
                //need to verify if its the seccound ingredient or the third
                if(totalIngredientInCauldron < 3)
                {
                    //Can put more ingredients
                    if(Recipe(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // the item is in list
                        //player.GetKitchenObject().SetKitchenObjectParent(this);
                        totalIngredientInCauldron++;
                        waitingToCook++;
                    }
                    
                    //else if (player.GetKitchenObject() == currentPotionCaulderonRecipe.potionObjectSO.secondIngredient && totalIngredientInCauldron == 2)
                    //{
                    //  //  third ingredient
                    //}


                    
                    
                    
                }

                //if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                //{
                //    //player is holding a plate
                //    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                //    {
                //        GetKitchenObject().DestroySelf();
                //        state = State.Idle;

                //        OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                //        {
                //            state = state
                //        });

                //        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                //        {
                //            progressNormalized = 0f
                //        });
                //    }
                //}
            }
            else
            {
                //player is not carrying anything

                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = 0f
                });

            }
        }
    }

    //private bool GetKitchenObjectSOWithInput(KitchenObjectSO inputKitchenObjectSO, int index)
    //{

    //}

    //private bool HasPotionCaulderonRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    //{
    //    //PotionCaulderonRecipeSO potionCaulderonRecipe = GetPotionCaulderonRecipeSO(inputKitchenObjectSO);
    //   // return potionCaulderonRecipe != null;
    //}

    private bool Recipe(KitchenObjectSO inputKitchenObject)
    {
         List<KitchenObjectSO> listToVerify = new List<KitchenObjectSO>(kitchenObjectSOInCaulderonList);

        listToVerify.Add(inputKitchenObject);

        foreach(PotionCaulderonRecipeSO potionCaulderonRecipeSO in potionCaulderonRecipeSOArray)
        {
            bool containsAll = listToVerify.All(inputKitchenObject => potionCaulderonRecipeSO.potionObjectSO.ingredientsSOList.Contains(inputKitchenObject));
            if (containsAll)
            {
                kitchenObjectSOInCaulderonList.Add(inputKitchenObject);
                return true;
            }
            
            
        } 
        return false;
    }

    private bool GetExistRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (PotionCaulderonRecipeSO potionCaulderonRecipeSO in potionCaulderonRecipeSOArray)
        {
            if (inputKitchenObjectSO == potionCaulderonRecipeSO.potionObjectSO.ingredientsSOList[0])
            {
                //first Ingredient have a recipe
                return true;
            }
        }
        return false;
    }




    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private PotionCaulderonRecipeSO[] GetPotionCaulderonRecipesSOPossibles(KitchenObjectSO inputKitchenObjectSO, int index)
    {
        foreach (PotionCaulderonRecipeSO potionCaulderonRecipeSO in potionCaulderonRecipeSOArray)
        {
          

            //if (fryingRecipeSO.input == inputKitchenObjectSO)
            //{
            //    return fryingRecipeSO;
            //}
        }
        return null;
    }


    private BurningRepiceSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        
        foreach (BurningRepiceSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
