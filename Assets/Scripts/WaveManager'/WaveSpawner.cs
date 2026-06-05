using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemiesToSpawn;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public GameObject enemyMovingPrefab;
    public Transform spawnPoint1;
    private GameObject spawnedEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spawnedEnemy = Instantiate(enemyMovingPrefab, spawnPoint1.position, spawnPoint1.rotation);
        foreach (GameObject enemy in waves[0].enemiesToSpawn)
        {
            Instantiate(enemy, spawnPoint1.position, spawnPoint1.rotation);
        }


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
        
    }
}
