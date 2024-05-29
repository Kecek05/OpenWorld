using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredPotionsController : MonoBehaviour
{
    public static StoredPotionsController main { get; set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private int[] recipeSavedCountArray; 

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        //Same size of the recipes
        recipeSavedCountArray = new int[recipeListSO.recipeSOList.Count];
    }

    public void StorePotion(PotionObjectSO _potionObjectSO)
    {

        for(int i = 0; i < recipeSavedCountArray.Length; i++)
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

}
