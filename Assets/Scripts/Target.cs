using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public TargetSize mySize;

    float scaleFactor = 1;

    public float myHealth = 100f;
    public float mySpeed = 2f;

    FiringPoint _FP;
    GameManager _GM;
    TargetManager _TM;

    [Header("AI")]
    public PatrolType myPatrol;
    int patrolPoint = 0;        //Needed for linear patrol movement
    bool reverse = false;       //Needed for repeat patrol movement
    Transform startPos;         //Needed for repeat patrol movement
    Transform endPos;           //Needed for repeat patrol movement
    Transform moveToPos;

    private void Start()
    {
        _TM = FindObjectOfType<TargetManager>();
        _FP = FindObjectOfType<FiringPoint>();
        _GM = FindObjectOfType<GameManager>();

        SetupAI();
        StartCoroutine(Move());
    }

    void Setup()
    {
        switch (mySize)
        {
            case TargetSize.Large:
                mySpeed = 1f;
                myHealth = 500f;
                scaleFactor = 3;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.green;
                break;
            case TargetSize.Medium:
                mySpeed = 2f;
                myHealth = 300f;
                scaleFactor = 2;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case TargetSize.Small:
                mySpeed = 3f;
                myHealth = 100f;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.red;
                break;

        }
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _TM.GetRandomSpawnPoint();
        moveToPos = endPos;
    }

    IEnumerator Move()
    {
        switch (myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _TM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _TM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;

            case PatrolType.Random:
                moveToPos = _TM.GetRandomSpawnPoint();
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

        yield return new WaitForSeconds(3);

        StartCoroutine(Move());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            myHealth -= _FP.bulletDamage;
        }

        if (collision.collider.CompareTag("Cannon"))
        {
            myHealth -= _FP.cannonDamage;
        }

        if (collision.collider.CompareTag("Pelt"))
        {
            myHealth -= _FP.bulletDamage;
        }

        if (myHealth <= 0)
        {
            Die();
            Destroy(this.gameObject);
        }

    }



    void Die()
    {
        StopAllCoroutines();
        _GM.OnEnemyDie();
    }

}