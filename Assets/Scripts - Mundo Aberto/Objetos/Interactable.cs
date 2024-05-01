using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    public GameObject Item;
    public bool canPickUp;
    public GameObject pickUpText;
    public int barril;

    [SerializeField] private string interactText;

    void Start()
    {
        barril =0;
    }
  
  public void Interact()
  {
    if (canPickUp)
         {
            pickUpText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
            CollectItem();
            }
         }

         else
         {
            pickUpText.SetActive(false);
         }
  }

  void OnTriggerEnter(Collider col)
         {
            if(col.tag == "item")
            {
                Item = col.gameObject;
                canPickUp = true;
            }
         }

    void OnTriggerExit(Collider col)
         {
            if(col.tag == "item")
            {
                canPickUp = true;
            }
         }
    
    void CollectItem()
    {
        
        Item = null;
        barril += 1;
        pickUpText.SetActive(false);
        Destroy(Item);
        Debug.Log("funciono saporra");
    }
  
public string GetInteractText()
  {
    return interactText;
  }
}



