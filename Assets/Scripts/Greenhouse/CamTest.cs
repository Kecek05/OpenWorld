using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{

    public Camera camera;
    public float speed = 2f;
    public GameObject player;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 cameraPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - 10);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, speed * Time.smoothDeltaTime);
    }
}
