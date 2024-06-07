using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    [SerializeField] private RecipeListSO recipeListSO;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }


    private void Start()
    {

        recipeListSO.OnRecipeSpawned += RecipeListSO_OnRecipeSpawned;

        UpdateVisual();
    }

    private void RecipeListSO_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        
    }


    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(PotionObjectSO potionObjectSO in recipeListSO.potionObjectSOList)
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetPotionObjectSO(potionObjectSO);


        }

    }



}
