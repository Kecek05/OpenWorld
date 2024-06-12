using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestleCounter : BaseCounter, IHasProgress, IHasHitBar
{
    //Events
    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;
    public event EventHandler<IHasHitBar.OnHitChangedEventArgs> OnHitChanged;
    public event EventHandler OnHitFinished;
    public event EventHandler OnHitInterrupted;
    public event EventHandler<IHasHitBar.OnHitMissedEventArgs> OnHitMissed;
    public event EventHandler<OnMoerRightEventArgs> OnMoerRight;

    public class OnMoerRightEventArgs
    {
        public Material particleMaterial;
    }

    //Minigame RNG
    private int numberCount;
    private int numberToHit = 5; // number to hit, in the middle
    private bool adding = true;


    //Progress
    private int crumpleCount = 0;
    private bool canHit = true;

    // Coroutines
    private IEnumerator CountCoroutine;
    private IEnumerator MissedCoroutine;

    // Timers
    [SerializeField] private float missedDelay = 1f;
    [Tooltip("in milisecconds")]
    [SerializeField] private float speedSlider;

    //Recipes
    [SerializeField] private InteractRecipeSO[] crumpleRecipeSOArray;
    private InteractRecipeSO selectedRecipeSO;



    public override void Interact(PlayerInHouse player)
    {
        if (!HasKitchenObject())
        {
            // no kitchenObject here
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //something that can be crumbled

                    player.GetKitchenObject().SetKitchenObjectParent(this);


                    selectedRecipeSO = GetCrumblingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    crumpleCount = 0;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = crumpleCount
                    });
                    CountCoroutine = Count();
                    StartCoroutine(CountCoroutine);
                }
            }
        } else
        {
            //there is a kitchenObject here
            if (!player.HasKitchenObject())
            {
                //player is not carrying anything

                StopCoroutine(CountCoroutine);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = 0f
                });
                OnHitInterrupted?.Invoke(this, EventArgs.Empty);
                numberCount = 0;
                adding = true;
                selectedRecipeSO = null;
                GetKitchenObject().SetKitchenObjectParent(player);
                
            }
        }



    }


    public override void InteractAlternate(PlayerInHouse player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a kitchenObject here and can be crumbled

            if (numberToHit == numberCount && canHit)
            {
                //correct hit
                crumpleCount++;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = (float)crumpleCount / selectedRecipeSO.interactProgressMax
                });
                OnMoerRight?.Invoke(this, new OnMoerRightEventArgs { particleMaterial = GetKitchenObject().GetKitchenObjectSO().particleMaterial});

                //numberCount = 0;
            } else
            {
                //miss the hit
                if (MissedCoroutine == null)
                {
                    MissedCoroutine = MissedCountDown();
                    StartCoroutine(MissedCoroutine);

                }

            }

            if(crumpleCount >= selectedRecipeSO.interactProgressMax)
            {
                StopCoroutine(CountCoroutine);

                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);



                OnHitFinished?.Invoke(this, EventArgs.Empty);
            }

        }
    }






    private IEnumerator Count()
    {
        while (crumpleCount <= selectedRecipeSO.interactProgressMax)
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

            OnHitChanged?.Invoke(this, new IHasHitBar.OnHitChangedEventArgs
            {
                hitNumber = numberCount
            });

            yield return new WaitForSeconds(speedSlider);

        }
    }

    private IEnumerator MissedCountDown()
    {
        canHit = false;
        OnHitMissed?.Invoke(this, new IHasHitBar.OnHitMissedEventArgs
        {
            missed = true
        });

        yield return new WaitForSeconds(missedDelay);
        canHit = true;

        OnHitMissed?.Invoke(this, new IHasHitBar.OnHitMissedEventArgs
        {
            missed = false
        });
        MissedCoroutine = null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        InteractRecipeSO crumblingRecipeSO = GetCrumblingRecipeSOWithInput(inputKitchenObjectSO);
        return crumblingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        InteractRecipeSO crumblingRecipeSO = GetCrumblingRecipeSOWithInput(inputKitchenObjectSO);
        if (crumblingRecipeSO != null)
        {
            return crumblingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private InteractRecipeSO GetCrumblingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (InteractRecipeSO crumblingRecipeSO in crumpleRecipeSOArray)
        {
            if (crumblingRecipeSO.input == inputKitchenObjectSO)
            {
                return crumblingRecipeSO;
            }
        }
        return null;
    }
}
