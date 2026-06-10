using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemiesToSpawn;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject enemyMovingPrefab;
    public Transform spawnPoint1;
    private GameObject spawnedEnemy;
    private int currentWaveIndex;
    private bool waveActive;
    private List<GameObject> spawnedEnemies = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWaveIndex = 0;
        //spawnedEnemy = Instantiate(enemyMovingPrefab, spawnPoint1.position, spawnPoint1.rotation);
        //foreach (GameObject enemy in waves[0].enemiesToSpawn)
        //{
           // Instantiate(enemy, spawnPoint1.position, spawnPoint1.rotation);
        //}

        StartWave();
        //foreach (RaycastHit hit in orderedHitsByDistance)
    }

    // Update is called once per frame
    void Update()
    {
        //if(!spawnedEnemy.activeSelf)
        //{
            //Debug.Log("Round won");
        //}
    }

    private void StartWave()
    {
        int enemyIndex = 0;
        spawnedEnemies = new();
        foreach (GameObject enemy in waves[currentWaveIndex].enemiesToSpawn)
        {
            Transform spawnPoint = spawnPoints[enemyIndex % spawnPoints.Length];
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            enemyIndex++;
        }
    }
}
