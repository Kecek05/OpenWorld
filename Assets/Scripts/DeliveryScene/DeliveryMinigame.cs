using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{
    public static DeliveryMinigame Instance {  get; private set; }

    private int hitCount = 0;

    public enum HitInputs
    {
        Q,
        W,
        E,
        R,
    }


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(true);
    }

    private void OnDisable()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(false);
    }


    public void Hitted()
    {
        hitCount++;
        if(hitCount == 1)
        {
            Debug.Log("all hited");
            gameObject.SetActive(false);
        }
    }
}
