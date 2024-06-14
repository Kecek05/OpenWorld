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
    public event EventHandler<IHasHitBar.OnHitRightEventArgs> OnHitRight;
    public event EventHandler<OnMoerRightEventArgs> OnMoerRight;

    public class OnMoerRightEventArgs
    {
        public Material particleMaterial;
    }

    //Minigame RNG
    private int numberCount;

    private int minNumberToHit = 4;
    private int maxNumberToHit = 6;

    //private int numberToHit = 5; // number to hit, in the middle
    private bool adding = true;


    //Progress
    private int crumpleCount = 0;
    private bool canHit = true;

    // Coroutines
    private IEnumerator countCoroutine;
    private IEnumerator missedCoroutine;
    private IEnumerator hitRightCoroutine;

    // Timers
    [SerializeField] private float missedDelay;
    [SerializeField] private float hitRightDelay = 0.1f;
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
                    countCoroutine = Count();
                    StartCoroutine(countCoroutine);
                }
            }
        } else
        {
            //there is a kitchenObject here
            if (!player.HasKitchenObject())
            {
                //player is not carrying anything

                StopCoroutine(countCoroutine);

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
            Debug.Log(numberCount);
            if (numberCount >= minNumberToHit && numberCount <= maxNumberToHit && canHit)
            {
                Debug.Log(numberCount + " Right");
                //correct hit
                crumpleCount++;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = (float)crumpleCount / selectedRecipeSO.interactProgressMax
                });
                OnMoerRight?.Invoke(this, new OnMoerRightEventArgs { particleMaterial = GetKitchenObject().GetKitchenObjectSO().particleMaterial});
                //numberCount = 0;

                if(hitRightCoroutine == null)
                {
                    hitRightCoroutine = HitRightCountDown();
                    StartCoroutine(hitRightCoroutine);
                }
            } else
            {
                //miss the hit
                if (missedCoroutine == null)
                {
                    missedCoroutine = MissedCountDown();
                    StartCoroutine(missedCoroutine);

                }

            }

            if(crumpleCount >= selectedRecipeSO.interactProgressMax)
            {
                StopCoroutine(countCoroutine);

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
        missedCoroutine = null;
    }


    private IEnumerator HitRightCountDown()
    {
        canHit = false;
        OnHitRight?.Invoke(this, new IHasHitBar.OnHitRightEventArgs
        {
            hitRight = true
        });
        yield return new WaitForSeconds(hitRightDelay);
        canHit = true;
        OnHitRight?.Invoke(this, new IHasHitBar.OnHitRightEventArgs
        {
            hitRight = false
        });
        hitRightCoroutine = null;
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
