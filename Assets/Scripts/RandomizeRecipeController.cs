

using System.Collections.Generic;

using UnityEngine;

public class RandomizeRecipeController : MonoBehaviour
{
    public static RandomizeRecipeController Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<PotionObjectSO> selectedPotionsSO = new List<PotionObjectSO>();

    private void Awake()
    {
        Instance = this;

        SetNewRandomRecipeList();
    }

    public void SetNewRandomRecipeList()
    {
        List<PotionObjectSO> currentPossiblesPotionObjectSOList = new List<PotionObjectSO>();
        selectedPotionsSO.Clear();
        for (int i = 0; i < recipeListSO.possiblesPotionObjectSOList.Count; i++)
        {
            //Copy the list
            currentPossiblesPotionObjectSOList.Add(recipeListSO.possiblesPotionObjectSOList[i]);
        }
        for (int i = 0; i < 2; ++i) // 2 times
        {
            int randomPotionObject = UnityEngine.Random.Range(0, currentPossiblesPotionObjectSOList.Count); // Random PotionObjectSO Index

            selectedPotionsSO.Add(currentPossiblesPotionObjectSOList[randomPotionObject]); // add the random

            currentPossiblesPotionObjectSOList.Remove(currentPossiblesPotionObjectSOList[randomPotionObject]); // subtract to  not repeat
        }
        recipeListSO.OnRecipeSpawnedTrigger();
    }

    public List<PotionObjectSO> GetSelectedPotionsSOList() { return selectedPotionsSO;  }
}
