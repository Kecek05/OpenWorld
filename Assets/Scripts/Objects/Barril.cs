using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour, IInteractable
{
    public void Interact(){
        Destroy(gameObject);
    }
}
