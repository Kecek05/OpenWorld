using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFader : MonoBehaviour
{
    private IEnumerator checkCoroutine;

    private ObjectFader _fader;

    private bool _isPlaying = true;

    [SerializeField] private GameObject player;
    [Space]
    [SerializeField] private float delayToCheck = 0.5f;

    private void Start()
    {
        if(checkCoroutine == null)
        {
            checkCoroutine = CheckObjectFader();
            StartCoroutine(checkCoroutine);
        }
            
    }

    private IEnumerator CheckObjectFader()
    {
        while(_isPlaying)
        {
            if(player != null)
            {
                Vector3 dir = player.transform.position - transform.position;
                Ray ray = new Ray(transform.position, dir);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == null) yield return new WaitForSeconds(delayToCheck);

                    if(hit.collider.gameObject == player)
                    {
                        //nothing is in front of the player
                        if (_fader != null)
                            _fader.DoFade(false);
                    } else
                    {
                        _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                        if(_fader != null )
                        {
                            _fader.DoFade(true);
                        }
                    }


                }
            }



            yield return new WaitForSeconds(delayToCheck);
        }
    }
}
