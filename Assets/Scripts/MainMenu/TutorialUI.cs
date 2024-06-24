using UnityEngine;

public class TutorialUI : MonoBehaviour
{

   [SerializeField] private GameObject currentImage;

    private void Start()
    {
        Time.timeScale = 0;
        currentImage.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            OnScreenInteracted();
        }
    }

    private void OnScreenInteracted()
    {
        Time.timeScale = 1f;
        currentImage.SetActive(false);
        Destroy(gameObject);
    }

}
