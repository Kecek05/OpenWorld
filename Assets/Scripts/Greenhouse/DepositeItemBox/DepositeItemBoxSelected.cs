
using UnityEngine;

public class DepositeItemBoxSelected : MonoBehaviour
{
    [SerializeField] private GameObject selectedVisualObject;


    private void Start()
    {
        PlayerOutsideHouse.InstancePlayerOutsideHouse.OnInteractObjectChanged += PlayerOutsideHouse_OnInteractObjectChanged;
    }
    private void OnDisable()
    {
        PlayerOutsideHouse.InstancePlayerOutsideHouse.OnInteractObjectChanged -= PlayerOutsideHouse_OnInteractObjectChanged;
    }

    private void PlayerOutsideHouse_OnInteractObjectChanged(GameObject obj)
    {
        if (selectedVisualObject != null)
        {

            if (obj == this.gameObject)
            {

                selectedVisualObject.SetActive(true);

            }
            else
            {

                selectedVisualObject.SetActive(false);

            }
        }
    }
}
