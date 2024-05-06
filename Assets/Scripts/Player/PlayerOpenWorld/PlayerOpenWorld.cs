using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOpenWorld : MonoBehaviour
{


    [SerializeField] private GameInput gameInput;


    private GameObject intectableObj;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {

        if(intectableObj != null)
        {
            IInteractable interactObj = intectableObj.gameObject.GetComponent<IInteractable>();
            interactObj.Interact();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if(interactable != null)
        {

            intectableObj = other.gameObject;
        } else
        {

            intectableObj = null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            intectableObj = null;
        }
        
    }

    public GameObject GetInteractableObj() { return intectableObj; }
}
