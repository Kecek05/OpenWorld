
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

    [SerializeField] private float duration = 2.0f;

    private void Start()
    {
        individualHit.OnHitStarted += IndividualHit_OnHitStarted;
    }
    private void OnEnable()
    {
        _startPosition = startPosition.position;
        _endPosition = endPosition.position;
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
            // Interpola��o linear da posi��o entre A e B ao longo do tempo
            transform.position = Vector3.Lerp(_startPosition, _endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Esperar at� o pr�ximo frame
        }

        // Garantir que o objeto esteja exatamente na posi��o final ao t�rmino da dura��o
        transform.position = _endPosition;
    }
}
