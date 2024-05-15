using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private GameObject interactableObj;

    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        if (gameInput != null)
            gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (interactableObj != null)
        {
            IInteractable2 interactObj = interactableObj.GetComponent<IInteractable2>();
            if (interactObj != null)
                interactObj.InteractCoin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu � interativo
        IInteractable2 interactable = other.GetComponent<IInteractable2>();
        if (interactable != null)
        {
            // Coleta a refer�ncia do objeto interativo
            interactableObj = other.gameObject;

            // Chama para intera��o
            Interagir();
            Debug.Log("Verificou se � interag�vel");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Limpa a refer�ncia do objeto interativo ao sair do trigger
        if (other.gameObject == interactableObj)
            interactableObj = null;
    }

    private void Interagir()
    {
        // Verifica se h� um objeto interativo
        if (interactableObj != null)
        {
            // Obt�m a interface interativa do objeto
            IInteractable2 interactObj = interactableObj.GetComponent<IInteractable2>();

            // Chama o m�todo de intera��o
            if (interactObj != null)
                interactObj.InteractCoin();
        }
    }
}
