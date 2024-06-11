using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame Difficulty")]
public class MinigameDifficultySO : ScriptableObject
{
    public string difficultyName;
    [Space]
    public float minSpawnTime;
    public float maxSpawnTime;
    [Space]
    public float minSpeed;
    public float maxSpeed;
}
