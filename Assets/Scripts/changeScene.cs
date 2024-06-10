using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class changeScene : MonoBehaviour
{

    [SerializeField] private int totalTime = 40;
    private int currentTime;
    [SerializeField] private Image clockImage;
    [SerializeField] private Text timerText;

    [SerializeField] private Loader.Scene scene;

    private void Start()
    {
        currentTime = totalTime;
        StartCoroutine(ChangeScene());
        StartCoroutine(Clock());
    }

    private IEnumerator Clock()
    {
        currentTime = totalTime;

        while (currentTime > 0)
        {
            currentTime--; 
            float fillAmount = (float)currentTime / totalTime; 
            clockImage.fillAmount = fillAmount;
            timerText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(totalTime);
        Loader.Load(scene);
    }

    
}
