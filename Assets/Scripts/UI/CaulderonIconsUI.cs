using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaulderonIconsUI : MonoBehaviour
{
    [SerializeField] private CaulderonCounter caulderonCounter;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        caulderonCounter.OnIngredientAdded += CaulderonCounter_OnIngredientAdded; ;
    }

    private void CaulderonCounter_OnIngredientAdded(object sender, CaulderonCounter.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in caulderonCounter.GetKitchenObjectSOInCaulderonList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
