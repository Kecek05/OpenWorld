using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    public void GameOvers()
    {
        StartCoroutine(DeleteGame());
    }

    public void Menu()
    {
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator DeleteGame()
    {
        
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private IEnumerator LoadMenuScene()
    {
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
    }


    
}
