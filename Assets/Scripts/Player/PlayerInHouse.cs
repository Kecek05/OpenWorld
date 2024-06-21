using System;

using UnityEngine;

public class PlayerInHouse : BasePlayer, IKitchenObjectParent
{

    public static PlayerInHouse InstancePlayerInHouse { get; private set; } // property


    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;
    [SerializeField] private Transform counterTopPoint;
    // [SerializeField] private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        InstancePlayerInHouse = this;
    }


    protected override void WitchInputs_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        InteractAnimTrigger();
        
        if ((selectedCounter != null))
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    protected override void WitchInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (intectableObj != null)
        {
            IInteractable interactObj = intectableObj.gameObject.GetComponent<IInteractable>();
            interactObj.Interact();
        }


        if ((selectedCounter != null))
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = WitchInputs.Instance.GetMovementVectorNormalizedInHouse();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
            lastInteractDir = moveDir;

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //has clear counter
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            } else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
            HoldItemAnimTrigger();
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
