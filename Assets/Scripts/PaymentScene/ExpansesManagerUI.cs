using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansesManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform expanseTemplate;
    [SerializeField] private PaymentCostsSO paymentCostsSO;

    private void Awake()
    {
        expanseTemplate.gameObject.SetActive(false);
    }


    private void Start()
    {

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == expanseTemplate) continue;
            Destroy(child.gameObject);
        }

        for(int i = 0; i < RandomizeExpanseController.Instance.GetCurrentExpensesIndex().Count; i++)
        {
            // intanciar o filho, pegar o index e passar para o filho as informações do paymentcostSO baseado no index que ele recebeu
        }
        //foreach (PaymentCostsSO paymentCostsSO in RandomizeRecipeController.Instance.GetSelectedPotionsSOList())
        //{
        //    Transform recipeTransform = Instantiate(expanseTemplate, container);
        //    recipeTransform.gameObject.SetActive(true);
        //    recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetPotionObjectSO(potionObjectSO);


        //}

    }

    // criar função que vai pegar a lista feita com os itens selecionados e instanciar os objetos filhos passando as informações

}
