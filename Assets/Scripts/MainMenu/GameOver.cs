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
        destroyOnLoads();
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.GreenHouse);
    }


    private IEnumerator LoadMenuScene()
    {
        destroyOnLoads();
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
    }


    private void destroyOnLoads()
    {
        GameObject DontDestroyOnLoadScripts = GameObject.FindWithTag("");
        Destroy(DontDestroyOnLoadScripts);
        GameObject dontDestroyThisDay = GameObject.FindWithTag("");
        Destroy(dontDestroyThisDay);
    }


    private void ResetData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        data.expansesCount = 0;
        data.economyPlayer = 0;
        data.dayCount = 0;
    }

    
}
