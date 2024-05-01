using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUi : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Interaction interaction;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;
    private void Update()
    {
        if (interaction.GetInteractableObject() != null)
        {
            Show(interaction.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(Interactable Interactable)
    
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = Interactable.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
    
}
