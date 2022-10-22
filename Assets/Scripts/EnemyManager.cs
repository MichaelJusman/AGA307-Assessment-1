using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    OrbMan,
    CubeMan
}

public enum PatrolType
{
    Linear,
    Random,
    Loop,
    Chase
}

public class EnemyManager : MonoBehaviour
{

    public Transform[] spawnPoints;         //The spawn point for our enemies to spawn at
    public GameObject[] enemyTypes;         //Contains all the different enemy types in our game
    public List<GameObject> enemies;        //A list containing all the enemies in our scene
    public string[] enemyNames;

    public int spawnCount = 10;
    public string killCondition = "Two";
    public float SpawnDelay = 2f;

    public GameObject enemyTrigger;

    GameManager _GM;

    private void Start()
    {
        _GM = FindObjectOfType<GameManager>();
    }

    IEnumerator SpawnDelayed()
    {
        yield return new WaitForSeconds(SpawnDelay);
        if (_GM.gameState == GameState.Playing)
        {
            SpawnEnemy();
        }
        if (enemies.Count <= spawnCount)
        {
            StartCoroutine(SpawnDelayed());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int enemyNumber = Random.Range(0, enemyTypes.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[enemyNumber], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, transform);
        enemies.Add(enemy);
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPoints[i].position, spawnPoints[i].rotation, transform);
            enemies.Add(enemy);
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

}
