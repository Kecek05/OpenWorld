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
      
        if(playerOpenWorld.GetInteractableObj() != null)
        {
            container.SetActive(true);
        } else
        {
            container.SetActive(false);
        }
       
    }
}
