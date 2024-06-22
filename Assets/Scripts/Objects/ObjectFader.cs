using System.Collections.Generic;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    float fadeSpeed = 5f, fadeAmount = 10f;
    float[] originalOpacity;


    List<Material[]> materialsList = new List<Material[]>();
    Renderer[] renderers;
    [SerializeField] bool doFade;


    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Debug.Log(renderer.materials);
            materialsList.Add(renderer.materials);
        }
        foreach (Material[] materials in materialsList)
        {
            foreach (Material material in materials)
            {
                Debug.Log(material.ToString());
            }
            for (int i = 0; i < materials.Length; i++)
            {
                //originalOpacity[i] = materials[i].color.a;
            }
        }
    }
    private void Update()
    {
        if (doFade)
            Fade();
        else
            UnFade();
    }
    void Fade()
    {
        foreach (Material[] materials in materialsList)
        {
            foreach(Material material in materials)
            {
                //Do Fade
                
                Color currentColor = material.color;
                Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
                material.color = smoothColor;
            }
        }
    }

    void UnFade()
    {
        foreach (Material[] materials in materialsList)
        {
            for(int i = 0;i < materials.Length;i++)
            {
                //Unfade
                //Do Fade
                Color currentColor = materials[i].color;
                Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, 255, fadeSpeed * Time.deltaTime));
                materials[i].color = smoothColor;
            }

        }
    }


    public void DoFade(bool _fade)
    {
        doFade = _fade;
        if (doFade)
            Fade();
        else
            UnFade();
    }

}
