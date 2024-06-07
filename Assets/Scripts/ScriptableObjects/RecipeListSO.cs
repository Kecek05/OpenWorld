using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    public event EventHandler OnRecipeSpawned;

    public List<PotionObjectSO> potionObjectSOList;

    public List<PotionObjectSO> possiblesPotionObjectSOList;



    private void OnEnable()
    {
        potionObjectSOList.Clear();
    }

    public void SetNewRandomRecipeList()
    {
        List<PotionObjectSO> currentPossiblesPotionObjectSOList = new List<PotionObjectSO>();
        potionObjectSOList.Clear();
        for (int i = 0; i < possiblesPotionObjectSOList.Count; i++)
        {
            //Copy the list
            currentPossiblesPotionObjectSOList.Add(possiblesPotionObjectSOList[i]);
        }
        for (int i = 0; i < 2; ++i) // 2 times
        {
            int randomPotionObject = UnityEngine.Random.Range(0, currentPossiblesPotionObjectSOList.Count); // Random PotionObjectSO Index

            potionObjectSOList.Add(currentPossiblesPotionObjectSOList[randomPotionObject]); // add the random

            currentPossiblesPotionObjectSOList.Remove(currentPossiblesPotionObjectSOList[randomPotionObject]); // subtract to  not repeat
        }
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }
}
