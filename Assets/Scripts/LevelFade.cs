using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private AsyncOperation asyncLoad; 

    private void OnEnable()
    {
        DoFadeIn();
    }

    private void DoFadeIn()
    {
        Debug.Log("tatrool");
        anim.Play("CircleSwipe_Start");
    }

    public void DoFadeOut()
    {
        anim.Play("CircleSwipe_End");
    }
}
