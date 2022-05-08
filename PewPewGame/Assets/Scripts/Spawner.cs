using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // setting the GameObject variables as a list
    public GameObject[] Enemies;
    public GameObject[] PowerupObjects;
    public GameObject[] Bosses;

    // setting the spawning variables
    public float spawnRangeX = 30;
    public float spawnRangeZ = 30;
    public float startDelay = 2;
    public float spawnInterval = 5f;

    // making a public variable that can be "turned on" or "off" (making it true or false) to stop and start the spawning.
    // this helps me troubleshoot the game
    public bool spawning = true;

    public float bossWave;

    // setting private variables
    private int waveNumber = 1;
    private int enemyIndex;
    private int powerupIndex;
    private int enemyCount;
    private int bossCount;
    private int bossSpawnCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        // the game starts by spawning an enemy and a powerup
        SpawnRandomEnemy();
        SpawnRandomPowerup();
    }
    
    // Update is called once per frame
    void Update()
    {
        // finding each enemy and projectile object with the corresponding script then finding the length of the list
        enemyCount = FindObjectsOfType<Enemy>().Length;
        bossCount = FindObjectsOfType<EnemyBoss>().Length;

        // when there are no enemies, the next wave starts. waveNumber controls the amount of enemies spawned at a time
        if (enemyCount == 0 && spawning)
        {
            DeletePowerups();
            waveNumber++;
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnRandomEnemy();
                SpawnRandomPowerup();
            }

            if (waveNumber == bossWave && bossCount == 0)
            {
                SpawnRandomBoss(bossSpawnCount);
                bossWave++;
                bossSpawnCount++;
                waveNumber = 0;
            }

        }
    }   

    // this function is called whenever there are no enemies, and at the start of the game
    // it spawns a random enemy type at a random location.
    // parameters - none
    // return value - none

    public void SpawnRandomEnemy()
    {
        // getting a random index of the powerup list
        int enemyIndex = Random.Range(0, Enemies.Length);

            // getting a random spawn location in the specefied range
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            1, Random.Range(-spawnRangeZ, spawnRangeZ));

        // spawning the enemy
        Instantiate(Enemies[enemyIndex], spawnPos, Enemies[enemyIndex].transform.rotation);

    }

    // this function is called whenever the next wave starts (when all enemies on screen are destroyed)
    // it spawns a random enemy type at a random location.
    // parameters - none
    // return value - none

    public void SpawnRandomPowerup()
    {
       // getting a random index of the powerup list
        int powerupIndex = Random.Range(0, PowerupObjects.Length);

        // getting a random spawn location in the specefied range
        Vector3 spawnPosPowerup = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            1, Random.Range(-spawnRangeZ, spawnRangeZ));

        // spawning the powerup
        Instantiate(PowerupObjects[powerupIndex], spawnPosPowerup, PowerupObjects[powerupIndex].transform.rotation);
    }

    // this function is called after 10 waves have been.
    // it spawns a random boss type at a random location.
    // parameters - none
    // return value - none

    public void SpawnRandomBoss(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
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

    // this function is called whenever the next wave starts
    // it destroys every powerup on the screen to lower the amount of objects that stay on the screen
    // parameters - none
    // return value - none
    private void DeletePowerups()
    {
        // finding each powerup to be destroyed
        GameObject[] rapidFire = GameObject.FindGameObjectsWithTag("RapidFire");
        GameObject[] piercing = GameObject.FindGameObjectsWithTag("Piercing");
        GameObject[] invincibility = GameObject.FindGameObjectsWithTag("Invincibility");
        GameObject[] shotgun = GameObject.FindGameObjectsWithTag("Shotgun");

        // destroying each powerupp //
        foreach (var rapidFireObject in rapidFire)
        {
            Destroy(rapidFireObject);
        }

        foreach (var piercingObject in piercing)
        {
            Destroy(piercingObject);
        }

        foreach (var invincibilityObject in invincibility)
        {
            Destroy(invincibilityObject);
        }

        foreach (var shotgunObject in shotgun)
        {
            Destroy(shotgunObject);
        }
    }
}
