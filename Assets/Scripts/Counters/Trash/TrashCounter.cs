using System;

public class TrashCounter : BaseCounter 
{
    public event EventHandler OnAnyObjectTrashed;

    public override void Interact(PlayerInHouse player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
