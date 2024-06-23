
using System.Collections;
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    [SerializeField] private float delayTrigger;

    private void Start()
    {
        StartCoroutine(nameof(DelayToTrigger));
    }

    private IEnumerator DelayToTrigger()
    {
        yield return new WaitForSeconds(delayTrigger);
        LevelFade.instance.DoFadeIn();
    }
}
