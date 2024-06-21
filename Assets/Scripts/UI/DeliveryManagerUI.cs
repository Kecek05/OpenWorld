using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;



    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }


    private void Start()
    {

        UpdateVisual();
    }




    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(PotionObjectSO potionObjectSO in RandomizeRecipeController.Instance.GetSelectedPotionsSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetPotionObjectSO(potionObjectSO);


        }

    }



}
