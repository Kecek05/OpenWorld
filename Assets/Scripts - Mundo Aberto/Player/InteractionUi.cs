using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class InteractionUi : MonoBehaviour
{
   
    [SerializeField] private GameObject container;
    [SerializeField] private PlayerOpenWorld playerOpenWorld;
    void Update()
    {
        Ray ray = new Ray(playerOpenWorld.GetInteractPoint().position, playerOpenWorld.GetInteractPoint().forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, playerOpenWorld.GetInteractRange()))
        {
            //bool hit = hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj);
            //container.SetActive(hit);
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                container.SetActive(true);
            }
            else
            {
                container.SetActive(false);
            }
        }
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo2, playerOpenWorld.GetInteractRange());
        Debug.DrawRay(playerOpenWorld.GetInteractPoint().position, playerOpenWorld.GetInteractPoint().forward * playerOpenWorld.GetInteractRange(), hit ? Color.green : Color.red);
    }
}
