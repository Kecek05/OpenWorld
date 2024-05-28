using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestleCounter : BaseCounter, IHasProgress, IHasHitBar
{
    private int numberCount;

    private int numberToHit = 5;

    private IEnumerator CountCoroutine;

    private int crumpleCount = 0;

    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    public event EventHandler<IHasHitBar.OnHitChangedEventArgs> OnHitChanged;

    public event EventHandler OnHitFinished;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {


                player.GetKitchenObject().SetKitchenObjectParent(this);

                crumpleCount = 0;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = crumpleCount
                });
                CountCoroutine = Count();
                StartCoroutine(CountCoroutine);


            }
        } else
        {
            //there is a kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                //if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                //{
                //    //player is holding a plate
                //    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                //    {
                //        GetKitchenObject().DestroySelf();
                //    }
                //}

            }
            else
            {
                //player is not carrying anything

                StopCoroutine(CountCoroutine);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = 0f
                });
                OnHitFinished?.Invoke(this, EventArgs.Empty);
                numberCount = 0;
                adding = true;

                GetKitchenObject().SetKitchenObjectParent(player);
                //if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                //{
                //    cuttingProgress = 0;

                //    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO());

                //    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                //    {
                //        progressNormalized = 0f
                //    });
                //}
            }
        }



    }


    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            //crumpleCount++;
            if(numberToHit == numberCount)
            {
                //correct hit
                crumpleCount++;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = (float)crumpleCount / 3
                });

                //numberCount = 0;
                Debug.Log(crumpleCount);
            } else
            {
                //miss the hit
                numberCount = 0;
                adding = true;

                Debug.Log("Missed");
            }

            if(crumpleCount == 3)
            {
                StopCoroutine(CountCoroutine);
                OnHitFinished?.Invoke(this, EventArgs.Empty);
                Debug.Log("Crumple Done");
            }

        }

        //if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        //{
        //    //there is a kitchenObject here and can be cut
        //    cuttingProgress++;

        //    OnCut?.Invoke(this, EventArgs.Empty);
        //    OnAnyCut?.Invoke(this, EventArgs.Empty);

        //    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
        //    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
        //    {
        //        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
        //    });

        //    if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
        //    {
        //        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

        //        GetKitchenObject().DestroySelf();

        //        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

        //    }

        //}
    }



    private bool adding = true;



    private IEnumerator Count()
    {
        while (crumpleCount <= 3)
        {

            if(adding)
            {
                numberCount++;
            } else
            {
                numberCount--;
            }



            if(numberCount >= 10 && adding)
            {
                adding = false;
            } else if(numberCount <= 0 && !adding)
            {
                adding = true;
            }

            Debug.Log("Number is " + numberCount);

            OnHitChanged?.Invoke(this, new IHasHitBar.OnHitChangedEventArgs
            {
                hitNumber = numberCount
            });

            yield return new WaitForSeconds(0.05f);

        }
    }
}
