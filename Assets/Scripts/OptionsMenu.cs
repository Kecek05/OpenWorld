using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu instance;


    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private GameObject bindingPanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button bindsBtn;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OpenOptionMenu()
    {
        optionsPanel.SetActive(true); //Gamepad navegation
        settingsBtn.Select();
    }

    public void CloseOptionsMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void OpenBinding()
    {
       // settingsBtn.image.color = deactiveColor;
        //bindsBtn.image.color = activeColor;
        settingsPanel.SetActive(false);
        bindingPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        //settingsBtn.image.color = activeColor;
        //bindsBtn.image.color = deactiveColor;
        bindingPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

}
