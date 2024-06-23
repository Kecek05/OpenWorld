
using System;
using System.Collections.Generic;
using UnityEngine;


public class StoredPotions : MonoBehaviour
{
    public static StoredPotions Instance { get; private set; }


    public List<PotionObjectSO> potionsMade;

    [SerializeField] private int[] recipeSavedCountArray;

    [SerializeField] PotionObjectSO[] potionsDebug;

    private bool stillHavePotions = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        recipeSavedCountArray = new int[RandomizeRecipeController.Instance.GetSelectedPotionsSOList().Count];
        //debug only
        //for (int i = 0; i < potionsDebug.Length; i++)
        //{
        //    potionsMade.Add(potionsDebug[i]);
        //}

    }

    public void ResetPotionsMade()
    {
        potionsMade.Clear();
    }

    public void StorePotion(PotionObjectSO _potionObjectSO)
    {
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            //Runs for all the recipes
            if (_potionObjectSO == RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i])
            {
                //stored potion matches with the recipe
                recipeSavedCountArray[i]++; //add the potion
                potionsMade.Add(_potionObjectSO);
                break;
            }
        }
    }

    public void DeliveryPotion(PotionObjectSO _potionObjectSO)
    {
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            //Runs for all the recipes
            if (_potionObjectSO == RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i])
            {
                //decrease the ammount of that potion
                recipeSavedCountArray[i]--;
                CheckHaveMorePotionsToDelivery();
                break;
            }
        }
    }
    private void CheckHaveMorePotionsToDelivery()
    {
        int count = 2;
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            if (recipeSavedCountArray[i] <= 0)
            {
                //Delivered all potions in this recipe
                count--;
            }
           
        }
        if(count <= 0)
        {
            //Delivered all potions, change scene (wait for 10 secconds) 
            //Do fade out
        }
    }

    public int[] GetRecipeSavedCountArray() { return recipeSavedCountArray; }

}
