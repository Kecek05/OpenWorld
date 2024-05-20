using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOpenWorld : MonoBehaviour
{
    public static PlayerOpenWorld main;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform aggroPoint;

    private GameObject intectableObj;

    private void Awake()
    {
        main = this;
    }
    private void Update()
    {
        Cursor.visible = false;
    }

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


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject);
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {

            intectableObj = other.gameObject;
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

    public Transform GetAggroPoint() { return aggroPoint; }
    public GameObject GetInteractableObj() { return intectableObj; }
}
