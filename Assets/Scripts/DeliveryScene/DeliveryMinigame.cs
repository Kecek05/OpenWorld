using System.Collections;
using UnityEngine;

public class DeliveryMinigame : MonoBehaviour
{
    public static DeliveryMinigame Instance {  get; private set; }

    private int hitCount = 0;

    [SerializeField] private GameObject titleObj;
    [SerializeField] private float delayToStart;
    [SerializeField] private GameObject[] IndividualHits;

    private IEnumerator startMinigameCoroutine;

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

    public void StartMinigame()
    {
        if(startMinigameCoroutine == null)
        {
            startMinigameCoroutine = StartMinigameCoroutine();
            StartCoroutine(startMinigameCoroutine);
        }
    }

    private IEnumerator StartMinigameCoroutine()
    {
        WitchInputs.Instance.ChangePLayerInputHitMinigame(true);
        titleObj.SetActive(true);
        yield return new WaitForSeconds(delayToStart);
        hitCount = 0;

        for (int i = 0; i < IndividualHits.Length; i++)
        {
            //turn all minigames on
            IndividualHits[i].gameObject.SetActive(true);
        }
        Debug.Log("Start Minigame");
    }

    private void FinishedMinigame()
    {

        WitchInputs.Instance.ChangePLayerInputHitMinigame(false);
        titleObj.SetActive(false);
        for (int i = 0; i < IndividualHits.Length; i++)
        {
            //turn all minigames off
            IndividualHits[i].gameObject.SetActive(false);
        }
        startMinigameCoroutine = null;
        Debug.Log("all hited");
    }


    public void Hitted()
    {
        hitCount++;
        if(hitCount >= 1)
        {
            FinishedMinigame();
        }
    }
}
