using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button playAgainBtn;

    private IEnumerator menuCoroutine;
    private IEnumerator playAgainCoroutine;

    public void GameOvers()
    {
        if (playAgainCoroutine == null)
        {
            playAgainCoroutine = DeleteGame();
            StartCoroutine(playAgainCoroutine);
        }
    }

    public void Menu()
    {
        if(menuCoroutine == null)
        {
            menuCoroutine = LoadMenuScene();
            StartCoroutine(menuCoroutine);
        }
    }

    private void Start()
    {
        playAgainBtn.Select();
    }

    private IEnumerator DeleteGame()
    {
        destroyOnLoads();
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.GreenHouse);
        playAgainCoroutine = null;
    }


    private IEnumerator LoadMenuScene()
    {
        destroyOnLoads();
        ResetData();
        if (LevelFade.instance != null)
            LevelFade.instance.StartCoroutine(LevelFade.instance.DoFadeOut());
        yield return new WaitForSeconds(1f);
        Loader.Load(Loader.Scene.MainMenuScene);
        menuCoroutine = null;
    }


    private void destroyOnLoads()
    {
        GameObject DontDestroyOnLoadScripts = GameObject.FindWithTag("DontDestroyScript");
        if(DontDestroyOnLoadScripts != null)
            Destroy(DontDestroyOnLoadScripts);
        GameObject dontDestroyThisDay = GameObject.FindWithTag("DontDestroyThisDay");
        if(dontDestroyThisDay != null)
            Destroy(dontDestroyThisDay);
    }


    private void ResetData()
    {
        //  SaveSystem.ResetPlayerSave();
        SaveSystem.DeletePlayerSave();
    }

    
}
