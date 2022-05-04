using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // setting the GameObject variables as a list
    public GameObject[] Enemies;
    public GameObject[] Powerups;
    public GameObject[] Bosses;

    // setting the spawning variables
    public float spawnRangeX = 30;
    public float spawnRangeZ = 30;
    public float startDelay = 2;
    public float spawnInterval = 5f;

    // making a public variable that can be "turned on" or "off" (making it true or false) to stop and start the spawning.
    // this helps me troubleshoot the game
    public bool spawning = true;

    public int bossWave;

    // setting private variables
    private int waveNumber = 1;
    private int enemyIndex;
    private int powerupIndex;
    private int enemyCount;
    private int powerupCount;

    // Start is called before the first frame update
    void Start()
    {
        // the game starts by spawning an enemy and a powerup
        SpawnRandomEnemy(spawning);
        SpawnRandomPowerup(spawning);
    }
    
    // Update is called once per frame
    void Update()
    {
        // finding each enemy and projectile object with the corresponding script then finding the length of the list
        enemyCount = FindObjectsOfType<Enemy>().Length;
        powerupCount = FindObjectsOfType<Powerups>().Length;

        // when there are no enemies, the next wave starts. waveNumber controls the amount of enemies spawned at a time
        if (enemyCount == 0 && spawning)
        {
            waveNumber++;
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnRandomEnemy(spawning);
                if (powerupCount <= 3)
                {
                    SpawnRandomPowerup(spawning);
                }

                if (waveNumber % bossWave == 0)
                {
                    SpawneRandomBoss(spawning);
                }
            }
        }
    }   

    // this function is called whenever there are no enemies, and at the start of the game
    // it spawns a random enemy type at a random location.
    // parameters - a boolean that enables/disables spawning
    // return value - none

    public void SpawnRandomEnemy(bool spawningEnemies)
    {
        // if spawningEnemies is true then it spawns enemies, but if it is false, it doesn't spawn more enemies
        if (spawningEnemies)
        {
            // getting a random index of the powerup list
            int enemyIndex = Random.Range(0, Enemies.Length);

            // getting a random spawn location in the specefied range
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
                1, Random.Range(-spawnRangeZ, spawnRangeZ));

            // spawning the enemy
            Instantiate(Enemies[enemyIndex], spawnPos, Enemies[enemyIndex].transform.rotation);
        }
    }

    // this function is called whenever the next wave starts (when all enemies on screen are destroyed)
    // it spawns a random enemy type at a random location.
    // parameters - a boolean that enables/disables spawning
    // return value - none
    public void SpawnRandomPowerup(bool spawningPowerups)
    {
        if (spawningPowerups)
        {
            // getting a random index of the powerup list
            int powerupIndex = Random.Range(0, Powerups.Length);

            // getting a random spawn location in the specefied range
            Vector3 spawnPosPowerup = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
                1, Random.Range(-spawnRangeZ, spawnRangeZ));

            // spawning the powerup
            Instantiate(Powerups[powerupIndex], spawnPosPowerup, Powerups[powerupIndex].transform.rotation);
        }
    }

    public void SpawnRandomBoss(bool spawningEnemies)
    {
        // if spawningEnemies is true then it spawns enemies, but if it is false, it doesn't spawn more enemies
        if (spawningEnemies)
        {
            // getting a random index of the powerup list
            int bossIndex = Random.Range(0, Bosses.Length);

            // getting a random spawn location in the specefied range
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
                1, Random.Range(-spawnRangeZ, spawnRangeZ));

            // spawning the enemy
            Instantiate(Bosses[bossIndex], spawnPos, Bosses[bossIndex].transform.rotation);
        }
    }
}
