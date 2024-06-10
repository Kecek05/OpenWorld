using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    public event EventHandler OnRecipeSpawned;

    public List<PotionObjectSO> possiblesPotionObjectSOList;

    public void OnRecipeSpawnedTrigger()
    {
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }
}
