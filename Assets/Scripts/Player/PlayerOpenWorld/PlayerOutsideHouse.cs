using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOutsideHouse : BasePlayer
{
    protected override void WitchInputs_OnInteractAction(object sender, System.EventArgs e)
    {

        if(intectableObj != null)
        {
            IInteractable interactObj = intectableObj.gameObject.GetComponent<IInteractable>();
            interactObj.Interact();
            intectableObj = null;
        }
    }

}
