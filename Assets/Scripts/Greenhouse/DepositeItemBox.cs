
using UnityEngine;

public class DepositeItemBox : MonoBehaviour, IInteractable
{
    [SerializeField] private WitchInventorySO witchInventorySO;

    public void Interact()
    {
        if(witchInventorySO.ListNotEmpty())
        {
            witchInventorySO.DepositeItemOnBox();
            Debug.Log("depositamo");
        }
        
    }
}
