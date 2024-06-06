
using System;
using UnityEngine;

[CreateAssetMenu()]
public class StoredPotionsSO : ScriptableObject
{
    [SerializeField] private RecipeListSO recipeListSO;

    private int[] recipeSavedCountArray;

    private int[] recipeOrderAmmountArray;

    private void OnEnable()
    {
        //Same size of the recipes
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

    public int[] GetTheAmmountOfMaxOrders()
    {
        // Delivery Manager needs to kn witch recipe he can choose 
        recipeOrderAmmountArray = new int[recipeSavedCountArray.Length];

        Array.Copy(recipeSavedCountArray, recipeOrderAmmountArray, recipeSavedCountArray.Length);

        return recipeOrderAmmountArray;
    }

    public int[] GetRecipeSavedCountArray() { return recipeSavedCountArray; }

    public RecipeListSO GetRecipeListSO() { return recipeListSO; }
}
