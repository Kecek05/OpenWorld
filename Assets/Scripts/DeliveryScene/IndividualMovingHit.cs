
using System.Collections;
using TMPro;
using UnityEngine;

public class IndividualMovingHit : MonoBehaviour
{

    [SerializeField] private IndividualHit individualHit;

    [SerializeField] private TextMeshProUGUI movingText;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform outScreenPosition;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _outScreenPosition;


    private IEnumerator movingCoroutine;
    private IEnumerator movingOutScreenCoroutine;

    private float duration;

    private void OnEnable()
    {
        gameObject.transform.position = startPosition.position;
        movingText.color = Color.white;
    }

    public void StartMoving()
    {
        _startPosition = startPosition.position;
        _endPosition = endPosition.position;
        _outScreenPosition = outScreenPosition.position;

        duration = individualHit.GetTimeToHit();

        if (movingCoroutine == null)
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

        //object in endPosition
        transform.position = _endPosition;
        movingCoroutine = null;
        movingOutScreenCoroutine = null;
        if(movingOutScreenCoroutine == null) {
            movingOutScreenCoroutine = MissedMoving();
            StartCoroutine(movingOutScreenCoroutine);
        }
    }

    private IEnumerator MissedMoving()
    {
        movingText.color = Color.gray;
        float elapsedTime = 0f;
        float outScreenTime = 0.5f;
        while ( elapsedTime < outScreenTime )
        {
            transform.position = Vector3.Lerp(_endPosition, _outScreenPosition, elapsedTime / outScreenTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Esperar até o próximo frame
        }
        movingOutScreenCoroutine = null;
        Debug.Log("OutScreen");
    }

    public void StopMoving() { movingCoroutine = null; }
}
