using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoint;

    private float timeSpawn;
    public float startTime;
    public int countWave;
    void Start()
    {
        timeSpawn = startTime;
    }

    void Update()
    {
        if (countWave > 0)
        {
            if (timeSpawn <= 0)
            {
                foreach (Transform spawnLocation in spawnPoint)
                {
                    Instantiate(enemy, spawnLocation.position, Quaternion.identity);
                }

                timeSpawn = startTime;
                countWave--;
            }
            else
            {
                timeSpawn -= Time.deltaTime;
            }
        }
    }
}
