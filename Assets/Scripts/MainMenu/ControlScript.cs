using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScript : MonoBehaviour
{
    private void Start()
    {
        LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeIn());
        StartCoroutine(changeForLore());
    }

    private IEnumerator changeForLore()
    {
        yield return new WaitForSeconds(7f);
        LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Lore");
    }
}
