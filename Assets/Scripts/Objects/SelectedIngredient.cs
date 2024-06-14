using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedIngredient : MonoBehaviour
{
    private GameObject parent;
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
        if(selectedVisualObject != null)
        {

            if(obj == parent)
            {

              selectedVisualObject.SetActive(true);

            } else
            {

              selectedVisualObject.SetActive(false);

            }
        }
    }

    public void SetParent(GameObject _parent) { parent = _parent; }
}
