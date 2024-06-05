
using UnityEngine;

public class DepositeItemBox : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        if(WitchInventory.Instance.ListNotEmpty())
        {
            WitchInventory.Instance.DepositeItemOnBox();
            Debug.Log("depositamo");
        }
        
    }
}
