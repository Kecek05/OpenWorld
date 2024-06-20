
using System;
using UnityEngine;

public class DepositeItemBox : MonoBehaviour, IInteractable
{
    private static readonly int INTERACT = Animator.StringToHash("interact");

    public event Action OnItemStashed;
    [SerializeField] private WitchInventorySO witchInventorySO;
    [SerializeField] private Animator animator;

    public void Interact()
    {
        if(witchInventorySO.ListNotEmpty())
        {
            animator.SetTrigger(INTERACT);
            witchInventorySO.DepositeItemOnBox();
            OnItemStashed?.Invoke(); //SFX
        }
        
    }
}
