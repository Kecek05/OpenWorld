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
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.GreenHouse);
    }


    private void ResetData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        data.expansesCount = 0;
        data.economyPlayer = 0;
        data.dayCount = 0;
    }

    private IEnumerator LoadMenuScene()
    {
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
    }

}
