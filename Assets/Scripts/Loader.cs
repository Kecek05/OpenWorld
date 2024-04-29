using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        House,
        LoadingScene,
    }
    public static int targetSceneIndex;

    public static void Load(Scene targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
