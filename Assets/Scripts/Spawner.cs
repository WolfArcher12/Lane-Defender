using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> objectsToSpawn;

    [SerializeField] float spawnInterval = 2f;

    float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        int objectIndex = Random.Range(0, objectsToSpawn.Count);
        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject objectToSpawn = objectsToSpawn[objectIndex];
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
