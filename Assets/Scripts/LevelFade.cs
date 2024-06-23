using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    public static LevelFade instance;


    [SerializeField] private Animator anim;
    [SerializeField] private GameObject fadeObject;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(DoFadeIn());
    }

    public IEnumerator DoFadeIn()
    {
        yield return null;
        fadeObject.SetActive(true);
        Debug.Log("name " + gameObject.name);
        anim.Play("CircleSwipe_Start");
        yield return new WaitForSeconds(1f);  // wait for the anim
        fadeObject.SetActive(false);
    }

    public IEnumerator DoFadeOut()
    {
        yield return null;
        fadeObject.SetActive(true);
        anim.Play("CircleSwipe_End");
        yield return new WaitForSeconds(1f);  // wait for the anim
        fadeObject.SetActive(false);
    }
}
