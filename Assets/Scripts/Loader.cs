using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    public enum Scene
    {
        MainMenuScene,
        House,
        GreenHouse,
        LoadingScene,
    }

    private static Action onLoaderCallBack;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene targetScene)
    {
        onLoaderCallBack = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(targetScene));
            LoadSceneAsync(targetScene);
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null; // If you need a delay to transition the scenes, put it here
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while(!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }
    public static void LoaderCallback()
    {
        if(onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
}
