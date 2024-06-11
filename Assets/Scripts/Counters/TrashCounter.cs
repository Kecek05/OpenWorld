using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter 
{
    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(PlayerInHouse player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
