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
    [SerializeField] private PotionObjectSO[] potionObjectSOArray;

    private State state;

    private float cookingTimer;
    private int totalIngredientInCauldron;

    private int waitingToCook;

    private int cookedIngredientCount;

    private float currentMaxTimeToCook;

    private List<KitchenObjectSO> kitchenObjectSOInCaulderonList = new List<KitchenObjectSO>();

    private PotionObjectSO potionDone;



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

                    cookingTimer += Time.deltaTime;


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = cookingTimer / currentMaxTimeToCook,
                        progressCount = cookedIngredientCount

                    });

                    if (cookingTimer >= currentMaxTimeToCook)
                    {
                        //Cooking Done
                        cookedIngredientCount++;
                       
                        state = State.CookedIngredient; 
                    }



                    //if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    //{
                    //    //Fried
                    //    GetKitchenObject().DestroySelf();

                    //    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);




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

                    } else
                    {
                        //can cook more
                        if(waitingToCook >= 1)
                        {
                            //there is one more to cook
                            waitingToCook--;
                            for (int i = 0; i < kitchenObjectSOInCaulderonList.Count; i++)
                            {
                                //take the time of the last kitchenObject in the list
                                cookingTimer = 0f;
                                currentMaxTimeToCook = kitchenObjectSOInCaulderonList[i].timeInCaulderon;
                            }

                            
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
                    potionDone = GetPotionObjectSOResult();
                    Debug.Log("Potion is " + potionDone);
                    break;

            }
        }
    }



    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (GetExistRecipeFirstIndexWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carrying something that can go to the caulderon
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    //fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


                    cookingTimer = 0f;
                    currentMaxTimeToCook = GetKitchenObject().GetKitchenObjectSO().timeInCaulderon;
                    totalIngredientInCauldron = 0;
                    totalIngredientInCauldron++;

                    //kitchenObjectSOInCaulderonList.Clear(); // reset the list
                    kitchenObjectSOInCaulderonList.Add(GetKitchenObject().GetKitchenObjectSO());

                    state = State.CookingIngredient;

                    OnStateChanged?.Invoke(this, new OnStateCHangedEventArgs
                    {
                        state = state
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
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        totalIngredientInCauldron++;
                        waitingToCook++;
                    }
                    
                }

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (state == State.Done)
                    {
                        //Potion done
                        plateKitchenObject.AddIngredientToPotion(potionDone);
                        GetKitchenObject().DestroySelf();
                        kitchenObjectSOInCaulderonList.Clear();
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
            else
            {
                //player is not carrying anything, reset the caulderon

                //GetKitchenObject().SetKitchenObjectParent(player);

                cookingTimer = 0f;
                totalIngredientInCauldron = 0;
                kitchenObjectSOInCaulderonList.Clear();

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

    private bool Recipe(KitchenObjectSO inputKitchenObject)
    {
         List<KitchenObjectSO> listToVerify = new List<KitchenObjectSO>(kitchenObjectSOInCaulderonList);

        listToVerify.Add(inputKitchenObject);

        int totalPotionObjectInArray = potionObjectSOArray.Length;

        foreach(PotionObjectSO potionObjectSO in potionObjectSOArray)
        {
            for(int i = 0; i <listToVerify.Count; i++)
            {
                if (listToVerify[i] != potionObjectSO.ingredientsSOList[i])
                {
                    //One ingredient is different
                    totalPotionObjectInArray--;
                    break;
                    //return false;
                }
            }
            


            //bool containsAll = listToVerify.All(inputKitchenObject => potionObjectSO.ingredientsSOList.Contains(inputKitchenObject));
            //if (containsAll)
            //{
                
            //    return true;
            //}
            
         
        }
        if (totalPotionObjectInArray > 0)
        {
            //all ingredients equal in at last one potion
            kitchenObjectSOInCaulderonList.Add(inputKitchenObject);
            return true;

        } else
        {
            // nothing equal in any potion
            return false;

        }


    }

    private bool GetExistRecipeFirstIndexWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (PotionObjectSO potionObjectSO in potionObjectSOArray)
        {
            if (inputKitchenObjectSO == potionObjectSO.ingredientsSOList[0])
            {
                //first Ingredient have a recipe
                return true;
            }
        }
        return false;
    }

    private PotionObjectSO GetPotionObjectSOResult()
    {
        foreach (PotionObjectSO potionObjectSO in potionObjectSOArray)
        {
            if(kitchenObjectSOInCaulderonList.SequenceEqual(potionObjectSO.ingredientsSOList))
            {
                // same ingredients
                return potionObjectSO;
            }

        }
        return null;
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
