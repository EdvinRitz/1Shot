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
    public GameObject[] randomEnemyPool;
    public PlayerHUD playerHUD;
    public WeaponShoot weaponShoot;
    public PlayerHealth playerHealth;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject enemyMovingPrefab;
    public Transform spawnPoint1;
    private GameObject spawnedEnemy;
    public int currentWaveIndex;
    public bool waveActive;
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

        StartCoroutine(StartWaveSequence());
    }

    public void ResolveWave()
    {
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
            StartCoroutine(WaveCompleteSequence());
        }
        else
        {
            Debug.Log("All waves complete");
        }
        
    }

    IEnumerator StartWaveSequence()
    {
        List<Transform> availableSpawnPoints = new(spawnPoints);
        waveActive = false;
        yield return new WaitForSecondsRealtime(3f);
        waveActive = true;
        weaponShoot.shotsRemaining++;
        int enemyIndex = 0;
        spawnedEnemies = new();
        foreach (GameObject enemy in waves[currentWaveIndex].enemiesToSpawn)
        {
            if(availableSpawnPoints.Count <= 0)
            {
                availableSpawnPoints = new(spawnPoints);
            }
            Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
            GameObject spawnedEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(spawnedEnemy);
            availableSpawnPoints.Remove(spawnPoint);
            enemyIndex++;
        }
    }

    IEnumerator WaveCompleteSequence()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartWave();
    }
}
