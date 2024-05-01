using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Interaction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Interactable Interactable))
                {
                    Interactable.Interact();
                }
            }
        }
        
    }

    public Interactable GetInteractableObject()
    {
        List<Interactable> InteractableList = new List<Interactable>();
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Interactable Interactable))
                {
                    InteractableList.Add(Interactable);
                }
            }

            Interactable closestInteractable = null;
            foreach (Interactable Interactable in InteractableList)
            {
                if (closestInteractable == null)
                {
                    closestInteractable = Interactable;
                }

                else
                {
                    if (Vector3.Distance(transform.position, Interactable.transform.position) < Vector3.Distance(transform.position, closestInteractable.transform.position))
                    {
                        closestInteractable = Interactable;
                    }
                }
            }
          
        return null;

    }
    
}
