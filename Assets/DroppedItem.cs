using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable2
{
    public float minHeight = 1.25f; // Altura m�nima 
    public float maxHeight = 1.5f; // Altura m�xima 
    public float floatSpeed = 1f; // Velocidade 
    public float rotationSpeed = 90f; // Velocidade de rota��o

    private void Update()
    {
        // Usa a fun��o seno para calcular a flutua��o
        float yPos = Mathf.Sin(Time.time * floatSpeed) * (maxHeight - minHeight) * 0.5f + (maxHeight + minHeight) * 0.5f;

        // Aplica a flutua��o obtida
        Vector3 newPosition = transform.position;
        newPosition.y = yPos;
        transform.position = newPosition;

        // Rotaciona
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void InteractCoin()
    {
        Destroy(gameObject);
    }
}
