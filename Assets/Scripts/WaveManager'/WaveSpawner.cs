using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemiesToSpawn;
}

public class WaveSpawner : MonoBehaviour
{
    public PlayerHealth playerHealth;
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
            GameObject spawnedEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(spawnedEnemy);
            enemyIndex++;
        }
    }

    public void ResolveWave()
    {
        StartCoroutine(DelayResolveWave());
        foreach (GameObject enemy in spawnedEnemies)
        {
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
            if (!baseEnemy.EnemyIsDead)
            {
                playerHealth.TakeDamage(1f);
            }
            baseEnemy.Die(); //change to taunt later or something else than normal die?
        }

        currentWaveIndex++;

        if (currentWaveIndex < waves.Length && !playerHealth.playerIsDead)
        {
            StartWave();
        }
        else
        {
            Debug.Log("All waves complete");
        }
        
    }

    IEnumerator DelayResolveWave()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
