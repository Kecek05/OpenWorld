using UnityEngine;

public class SelectedCountainerCounterVisual : MonoBehaviour
{
    [SerializeField] private CountainerCounter countainerCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        PlayerInHouse.InstancePlayerInHouse.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerInHouse.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == countainerCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);

        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);

        }
    }
}
