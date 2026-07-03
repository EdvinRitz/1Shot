using System.Collections;
using System.Collections.Generic;
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
    public UpgradeManager upgradeManager;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject enemyMovingPrefab;
    public Transform spawnPoint1;
    private GameObject spawnedEnemy;
    public int currentWaveIndex;
    public bool waveActive;
    private List<GameObject> spawnedEnemies = new();
    public int enemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartWave();
    }
    public void StartWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(StartWaveSequence());
        }
        else
        {
            StartCoroutine(StartRandomWaveSequence());
        }
    }

    public void ResolveWave()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
            if (!baseEnemy.EnemyIsDead)
            {
                playerHealth.TakeDamage(1f);
                baseEnemy.Die(); //Add taunt later or something else for surviving enemies? Instead of normal die?
            }
        }

        currentWaveIndex++;

        if(currentWaveIndex % 2 == 0 && !playerHealth.playerIsDead)
        {
            upgradeManager.BeginSelection();
        }
        else if (!playerHealth.playerIsDead)
        {
            StartCoroutine(WaveCompleteSequence());
        }
        else
        {
            Debug.Log("Player dead");
            return;
        }
    }

    IEnumerator StartWaveSequence()
    {
        List<Transform> availableSpawnPoints = new(spawnPoints);
        waveActive = false;
        yield return new WaitForSecondsRealtime(3f);
        waveActive = true;
        weaponShoot.shotsRemaining++;
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
        }
    }

    IEnumerator StartRandomWaveSequence()
    {
        enemyCount = 1 + currentWaveIndex / 2;
        List<Transform> availableSpawnPoints = new(spawnPoints);
        waveActive = false;
        yield return new WaitForSecondsRealtime(3f);
        waveActive = true;
        weaponShoot.shotsRemaining++;
        spawnedEnemies = new();
        
        for (int i = 0; i < enemyCount; i++)
        {
            if(availableSpawnPoints.Count <= 0)
            {
                availableSpawnPoints = new(spawnPoints);
            }
            Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
            int randomIndex = Random.Range(0, randomEnemyPool.Length);
            GameObject enemyPrefab = randomEnemyPool[randomIndex];
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(spawnedEnemy);
            availableSpawnPoints.Remove(spawnPoint);
        }
    }

    IEnumerator WaveCompleteSequence()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartWave();
    }
    
}
