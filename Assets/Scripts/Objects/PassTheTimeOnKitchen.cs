using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PassTheTimeOnKitchen : MonoBehaviour, IInteractable
{

    public ChangeScene changeScene;

    public void Interact()
    {
        changeScene.currentTime = 0.1f;
    }


}
