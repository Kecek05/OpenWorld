using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hitObject;
    [SerializeField] private Slider hitSlider;
    [SerializeField] private Image handleImage;
    private IHasHitBar hitBar;
    private void Start()
    {
        hitBar = hitObject.GetComponent<IHasHitBar>();


        hitBar.OnHitChanged += HitBar_OnHitChanged;

        hitBar.OnHitFinished += HitBar_OnHitFinished;

        hitBar.OnHitMissed += HitBar_OnHitMissed;

        hitSlider.value = 0;

        Hide();
    }

    private void HitBar_OnHitMissed(object sender, IHasHitBar.OnHitMissedEventArgs e)
    {
        if(e.missed)
        {
            handleImage.color = Color.black;
        } else
        {
            handleImage.color = Color.white;
        }
    }

    private void HitBar_OnHitFinished(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void HitBar_OnHitChanged(object sender, IHasHitBar.OnHitChangedEventArgs e)
    {
       hitSlider.value = (float)e.hitNumber / 10;
        Debug.Log(hitSlider.value + " Value");


        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
