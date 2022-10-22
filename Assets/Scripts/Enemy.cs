using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType myType;

    public float myHealth = 100f;
    public float mySpeed = 2f;

    EnemyManager _EM;

    [Header("AI")]
    public PatrolType myPatrol;
    int patrolPoint = 0;        //Needed for linear patrol movement
    bool reverse = false;       //Needed for repeat patrol movement
    Transform startPos;         //Needed for repeat patrol movement
    Transform endPos;           //Needed for repeat patrol movement
    Transform moveToPos;

    private void Start()
    {
        _EM = FindObjectOfType<EnemyManager>();
        SetupAI();
        StartCoroutine(Move());
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
    }

    IEnumerator Move()
    {
        switch(myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;

            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;

            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }

        transform.LookAt(moveToPos);
        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Move());

    }

}
