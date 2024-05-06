using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOpenWorld : MonoBehaviour
{

    [SerializeField] private Transform interactPoint;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactRange;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Ray ray = new Ray(interactPoint.position, interactPoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();

            }
        }
    }

    public Transform GetInteractPoint() { return interactPoint; }
    public float GetInteractRange() { return interactRange; }

}
