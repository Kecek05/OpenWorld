using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private TextMeshProUGUI recipeCountText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Image potionImage;

    private PotionObjectSO selectedPotionObjectSO;

    [SerializeField] private StoredPotionsSO storedPotionsSO;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeCompletedEventArgs e)
    {
        UpdatePotionCount();
    }

    public void SetPotionObjectSO(PotionObjectSO potionObjectSO)
    {
        selectedPotionObjectSO = potionObjectSO;

        recipeNameText.text = potionObjectSO.PotionName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in potionObjectSO.ingredientsSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        potionImage.sprite = potionObjectSO.potionSprite;
    }


    private void UpdatePotionCount()
    {
        for (int i = 0; i < storedPotionsSO.GetRecipeSavedCountArray().Length; i++)
        {
            //Runs for all the recipes
            if (selectedPotionObjectSO == storedPotionsSO.GetRecipeListSO().recipeSOList[i])
            {
                //stored potion matches with the recipe
                recipeCountText.text = storedPotionsSO.GetRecipeSavedCountArray()[i].ToString() + "x";
                break;
            }
        }
    }
}
