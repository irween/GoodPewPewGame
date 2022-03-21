using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Powerups;
    public float spawnRangeX = 30;
    public float spawnRangeZ = 30;
    public float startDelay = 2;
    public float spawnInterval = 5f;
    public int waveNumber = 1;
    public int enemyIndex;
    public int powerupIndex;
    public int enemyCount;
    public int powerupCount;
    private List<powerupList> ["Piercing"]

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomEnemyWave(1);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        powerupCount = FindObjectsOfType<powerupList[0]>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnRandomEnemyWave(waveNumber);
        }
    }

    void SpawnRandomEnemyWave(int waves)
    {
        for (int i = 0; i < waves; i++)
        {
            SpawnRandomEnemy();
        }
    }

    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, Enemies.Length);

        int powerupIndex = Random.Range(0, Powerups.Length);

        Vector3 spawnposPowerup = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            1, Random.Range(-spawnRangeZ, spawnRangeZ));

        Vector3 spawnpos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            1, Random.Range(-spawnRangeZ, spawnRangeZ));

        Instantiate(Powerups[powerupIndex], spawnposPowerup, Powerups[powerupIndex].transform.rotation);
        Instantiate(Enemies[enemyIndex], spawnpos, Enemies[enemyIndex].transform.rotation);
    }
}
