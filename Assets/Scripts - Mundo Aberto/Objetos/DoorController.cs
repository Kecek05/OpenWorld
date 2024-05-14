using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string sceneToLoad;
    private bool doorOpened = false;
    public void Interact()
    {
        Debug.Log("Funcionou");
        if (!doorOpened)
        {
            if(doorAnimator != null)
            {
                doorAnimator.SetTrigger("Abrir");
            }

            doorOpened = true;
        }

        ChangeScene();
    }

    private void ChangeScene()
    {
        Debug.Log(sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

}
