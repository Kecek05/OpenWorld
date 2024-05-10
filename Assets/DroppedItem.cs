using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable2
{
    public float floatSpeed = 1f; // Velocidade de flutuação do item
    public float floatHeight = 0.5f; // Altura de flutuação do item
    public float rotationSpeed = 90f; // Velocidade de rotação do item

    private void Update()
    {
        // Faz o item flutuar
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * floatSpeed) * floatHeight, transform.position.z);

        // Rotaciona o item
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void InteractCoin()
    {
        Destroy(gameObject);
    }
}
