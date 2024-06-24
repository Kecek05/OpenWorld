using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitalLore : MonoBehaviour
{
    [SerializeField] private Button changebtn;

    private void Start()
    {
        changebtn.Select();
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
