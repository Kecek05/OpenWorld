using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public static event EventHandler OnAnyCut;

    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    public event EventHandler OnCut;

    public event EventHandler OnCutFinished;

    [SerializeField] private InteractRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;

                    InteractRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.interactProgressMax
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
                //if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                //{
                //    //player is holding a plate
                //    //if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                //    //{
                //    //    GetKitchenObject().DestroySelf();
                //    //}
                //}

            }
            else
            {
                //player is not carrying anything

                GetKitchenObject().SetKitchenObjectParent(player);
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                        cuttingProgress = 0;

                    InteractRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }
    }


    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a kitchenObject here and can be cut
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            InteractRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.interactProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.interactProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

                OnCutFinished?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        InteractRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        InteractRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        } else
        {
            return null;
        }
    }

    private InteractRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (InteractRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
