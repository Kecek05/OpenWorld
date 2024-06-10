
using System.Collections;
using UnityEngine;

public class IndividualMovingHit : MonoBehaviour
{

    [SerializeField] private IndividualHit individualHit;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    private Vector3 _startPosition;
    private Vector3 _endPosition;


    private IEnumerator movingCoroutine;

    private float duration;


    private void OnEnable()
    {
        individualHit.OnHitStarted += IndividualHit_OnHitStarted;

        _startPosition = startPosition.position;
        _endPosition = endPosition.position;
        gameObject.transform.position = _startPosition;

        duration = individualHit.GetTimeToHit();


        Debug.Log("start position is " +  _startPosition + " end position is " +  _endPosition);
    }

    private void OnDisable()
    {
        individualHit.OnHitStarted -= IndividualHit_OnHitStarted;
    }

    private void IndividualHit_OnHitStarted(object sender, System.EventArgs e)
    {
        if(movingCoroutine == null)
        {
            movingCoroutine = Moving();
            StartCoroutine(movingCoroutine);
        }
    }

    private IEnumerator Moving()
    {
        float elapsedTime = 0;


        while (elapsedTime < duration)
        {
            // Interpolação linear da posição entre A e B ao longo do tempo
            transform.position = Vector3.Lerp(_startPosition, _endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Esperar até o próximo frame
        }

        // Garantir que o objeto esteja exatamente na posição final ao término da duração
        transform.position = _endPosition;

        movingCoroutine = null;
    }
}
