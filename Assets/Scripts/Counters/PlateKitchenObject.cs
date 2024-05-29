using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private PotionObjectSO potionObjectSOInThisPlate;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public void AddIngredientToPotion(PotionObjectSO potionObjectSO)
    {
        foreach(KitchenObjectSO kitchenObjectInPotion in potionObjectSO.ingredientsSOList)
        {
            if(!validKitchenObjectSOList.Contains(kitchenObjectInPotion))
            {
                // not valid ingredient
                //return false;
            }
            if(kitchenObjectSOList.Contains(kitchenObjectInPotion))
            {
                //already has this type
               // return false;
            } else
            {
                //new ingredient
                kitchenObjectSOList.Add(kitchenObjectInPotion);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                    kitchenObjectSO = kitchenObjectInPotion
                });
                //return true;
            }

        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

    public PotionObjectSO GetPotionObjectSOInThisPlate() { return potionObjectSOInThisPlate;}

    public void SetPotionObjectSOInThisPlate(PotionObjectSO potionObjectSO) { potionObjectSOInThisPlate = potionObjectSO;}
}
