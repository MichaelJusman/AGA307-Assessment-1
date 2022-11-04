using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolType
{
    Linear,
    Random,
    Loop,
    Chase
}

public enum TargetSize
{
    Small,
    Medium,
    Large
}



public class TargetManager : Singleton<TargetManager>
{

    public Transform[] spawnPoints;         //The spawn point for our target to spawn at
    public GameObject[] targetTypes;         //Contains all the different enemy types in our game
    public List<GameObject> target;        //A list containing all the target in our scene

    public int spawnCount = 10;
    public string killCondition = "Two";
    public float SpawnDelay = 2f;

    public GameObject targetTrigger;

    GameManager _GM;
    UIManager _UI;

    private void Start()
    {
        _GM = FindObjectOfType<GameManager>();
        _UI = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        int enemyNumber = Random.Range(0, targetTypes.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(targetTypes[enemyNumber], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, transform);
        target.Add(enemy);
        _UI.UpdateTargetCount(target.Count);
        
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    public void KillTarget(GameObject _target)
    {
        if(target.Count == 0)
            return;


        target.Remove(_target);
        _UI.UpdateTargetCount(target.Count);
        _GM.UpdateBonusTime();

    }


}
