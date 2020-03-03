using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float waveWait = 5.0f;
    public float countDown = 2.0f;

    int waveNum = 1;

    public Transform enemy;

    private void Update()
    {
        if (countDown<= 0f)
        {
            WaveSpawn();
            countDown = waveWait;
        }
        countDown -= Time.deltaTime;
    }

    void WaveSpawn()
    {
        for (int i = 0; i < waveNum; i++)
        {
            SpawnEnemy();
        }
        //waveNum++;
    }

    void SpawnEnemy()
    {
        Debug.Log("Enemy Spawned");
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
