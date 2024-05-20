using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class InteractionUi : MonoBehaviour
{
   
    [SerializeField] private GameObject container;
    [SerializeField] private PlayerOpenWorld playerOpenWorld;
    [SerializeField] private Player playerHouse;
    void Update()
    {
      
        if(playerOpenWorld != null)
        {
            if(playerOpenWorld.GetInteractableObj() != null)
            {
                container.SetActive(true);
            } else
            {
                container.SetActive(false);
            }

        }

        if(playerHouse != null)
        {
            if (playerHouse.GetInteractableObj() != null)
            {
                container.SetActive(true);
            }
            else
            {
                container.SetActive(false);
            }

        }
       
    }
}
