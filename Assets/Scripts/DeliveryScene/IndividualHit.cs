
using System.Collections;
using UnityEngine;

public class IndividualHit : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float downSpeed;
    [SerializeField] private float hitNumber;

    private
        float _hitCount = 0f;

    private bool hited = false;

    private IEnumerator hitLoopCoroutine;

    private bool inLoop = false;

    private void OnEnable()
    {
        hited = false;
        hitLoopCoroutine = HitLoop();
        StartCoroutine(hitLoopCoroutine);
    }


    private void Start()
    {
        WitchInputs.Instance.OnHit1Performed += WitchInputs_OnHit1Performed;
    }

    private void WitchInputs_OnHit1Performed(object sender, System.EventArgs e)
    {
        if(inLoop)
        {
            hited = true;
        }
    }

    private IEnumerator HitLoop()
    {
        while(!hited && _hitCount <= hitNumber)
        {
            //not clicked yet or can still click
            inLoop = true;

            _hitCount += Time.deltaTime;
            yield return null;
        }
        //Hitted
        inLoop = false;
        parent.SetActive(false);
    }

    
}
