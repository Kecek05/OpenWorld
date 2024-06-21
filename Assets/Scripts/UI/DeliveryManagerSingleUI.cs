using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private TextMeshProUGUI recipeCountText;
    [SerializeField] private TextMeshProUGUI recipeCostText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Image potionImage;

    private PotionObjectSO selectedPotionObjectSO;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(DeliveryManager.Instance != null)
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.OnRecipeCompletedEventArgs e)
    {
        UpdatePotionCount();
    }

    public void SetPotionObjectSO(PotionObjectSO potionObjectSO)
    {
        selectedPotionObjectSO = potionObjectSO;



        //foreach (Transform child in iconContainer)
        //{
        //    if (child == iconTemplate) continue;
        //    Destroy(child.gameObject);
        //}
        foreach(KitchenObjectSO kitchenObjectSO in potionObjectSO.ingredientsSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        potionImage.sprite = potionObjectSO.potionSprite;
        recipeCostText.text = "$" + potionObjectSO.potionMoneyRecieve.ToString();
    }


    private void UpdatePotionCount()
    {
        for (int i = 0; i < StoredPotions.Instance.GetRecipeSavedCountArray().Length; i++)
        {
            //Runs for all the recipes
            if (selectedPotionObjectSO == RandomizeRecipeController.Instance.GetSelectedPotionsSOList()[i])
            {
                //stored potion matches with the recipe
                recipeCountText.text = StoredPotions.Instance.GetRecipeSavedCountArray()[i].ToString() + "x";
                break;
            }
        }
    }
}
