using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float waveWait = 5.0f;
    public float newWait = 1.0f;
    public float countDown = 10.0f;
    public int howMany;

    int waveNum = 1;

    public Transform enemy;

    private void Update()
    {
        countDown -= newWait * Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = waveWait;
            SpawnEnemy();
        }
    }



    void SpawnEnemy()
    {
        //Debug.Log("Enemy Spawned");

        howMany = Random.Range(1, 10);
        Debug.Log(howMany);
        
        float newCountdown = 2.0f;


        for (int i = 0; i < howMany; i++)
        {
            Debug.Log("Enemy Spawn");
            Instantiate(enemy, spawnPoint[Random.Range(0,3)].position, spawnPoint[Random.Range(0,3)].rotation);
            newCountdown -= newWait * Time.deltaTime;
        }
    }

    //wait for each wave
    //instantiate fair number of mobs
    //cooldown between enemy spawns




}
