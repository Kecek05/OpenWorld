
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoredPotionsSO : ScriptableObject
{
    [SerializeField] private RecipeListSO recipeListSO;

    public List<PotionObjectSO> potionsMade;

    [SerializeField] private int[] recipeSavedCountArray;


    private void OnEnable()
    {
        //Same size of the recipes
        potionsMade.Clear();
        recipeSavedCountArray = new int[recipeListSO.recipeSOList.Count];
    }

    public void StorePotion(PotionObjectSO _potionObjectSO)
    {
        for (int i = 0; i < recipeSavedCountArray.Length; i++)
        {
            //Runs for all the recipes
            if (_potionObjectSO == recipeListSO.recipeSOList[i])
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
            if (_potionObjectSO == recipeListSO.recipeSOList[i])
            {
                //decrease the ammount of that potion
                recipeSavedCountArray[i]--;
                break;
            }
        }
    }

    public int[] GetRecipeSavedCountArray() { return recipeSavedCountArray; }

    public RecipeListSO GetRecipeListSO() { return recipeListSO; }

}
