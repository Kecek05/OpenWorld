using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;


    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ParticleSystem pickUpParticle;

    private KitchenObject kitchenObject;

    public virtual void Interact(PlayerInHouse player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(PlayerInHouse player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null )
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
        if(pickUpParticle != null)
        {
            pickUpParticle.Play();
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
