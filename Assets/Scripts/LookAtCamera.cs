using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Coroutine updateCourotine;

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;
    private void OnEnable()
    {
        updateCourotine = StartCoroutine(UpdateLookAt());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateLookAt());    
    }

    private IEnumerator UpdateLookAt()
    {
        while (true)
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }

            yield return new WaitForSeconds(0.1f);

        }
        

        
    }
}
