using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float spawnRangeX = 35;
    public float spawnRangeY = 35;
    public float startDelay = 2;
    public float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int enemyIndex;

    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, Enemies.Length);
        Vector3 spawnpos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
            1, Random.Range(-spawnRangeY, spawnRangeY));
        Instantiate(Enemies[enemyIndex], spawnpos, Enemies[enemyIndex].transform.rotation);
    }

    public float lives = 5;
    private float livesCount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (livesCount == lives)
            {
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
            }
            else
            {
                livesCount = livesCount + 1;
            }
        }
    }
}
