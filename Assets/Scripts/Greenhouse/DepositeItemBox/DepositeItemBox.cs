
using System;
using UnityEngine;

public class DepositeItemBox : MonoBehaviour, IInteractable
{
    public event Action OnItemStashed;
    [SerializeField] private WitchInventorySO witchInventorySO;

    public void Interact()
    {
        if(witchInventorySO.ListNotEmpty())
        {
            witchInventorySO.DepositeItemOnBox();
            Debug.Log("depositamo");
            OnItemStashed?.Invoke(); //SFX
        }
        
    }
}
