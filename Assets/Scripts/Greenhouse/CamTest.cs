using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{

    [SerializeField] private Camera cameraMain;
    [SerializeField] private Camera cameraMinimap;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private float speed = 4f;
    

    private void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 cameraPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - 10);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, speed * Time.smoothDeltaTime);
        
    }

    private void LateUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 cameraPosition2 = new Vector3(playerPosition.x, cameraMinimap.transform.position.y, playerPosition.z);
        cameraMinimap.transform.position = Vector3.Lerp(cameraMinimap.transform.position, cameraPosition2, speed * Time.deltaTime);
    }
}
