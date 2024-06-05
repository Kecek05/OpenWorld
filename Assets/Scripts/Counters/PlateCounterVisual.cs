using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform[] counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;


    private List<GameObject> plateVisualGameObjectList;

    private int counterTopPointIndex = 0;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        counterTopPointIndex--;

        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        counterTopPointIndex++;

        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint[counterTopPointIndex - 1]);

        plateVisualTransform.localPosition = Vector3.zero;

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
