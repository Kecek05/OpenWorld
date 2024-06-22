
using System.Diagnostics;

public class PlayerOutsideHouse : BasePlayer
{

    public static PlayerOutsideHouse InstancePlayerOutsideHouse;

    protected override void Awake()
    {
        base.Awake();
        InstancePlayerOutsideHouse = this;
    }

    protected override void WitchInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        InteractAnimTrigger();
        if(intectableObj != null)
        {
            IInteractable interactObj = intectableObj.gameObject.GetComponent<IInteractable>();
            interactObj.Interact();
            intectableObj = null;
        }
    }

}
