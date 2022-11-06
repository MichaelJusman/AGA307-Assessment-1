using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{

    public TargetSize mySize;
    public Difficulty difficulty;

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
        Setup();
        StartCoroutine(Move());
    }



    void Setup()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                mySize = TargetSize.Small;
                mySpeed = 3f;
                myHealth = 100f;
                scaleFactor = 0.5f;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.red;
                break;

            case Difficulty.Medium:
                mySize = TargetSize.Medium;
                mySpeed = 2f;
                myHealth = 300f;
                scaleFactor = 1;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.yellow;
                break;

            case Difficulty.Hard:
                mySize = TargetSize.Large;
                mySpeed = 1f;
                myHealth = 500f;
                scaleFactor = 2;
                transform.localScale = Vector3.one * scaleFactor;
                transform.GetComponent<Renderer>().material.color = Color.green;
                break;
        }



        //switch (mySize)
        //{
        //    case TargetSize.Large:
        //        mySpeed = 1f;
        //        myHealth = 500f;
        //        scaleFactor = 2;
        //        transform.localScale = Vector3.one * scaleFactor;
        //        transform.GetComponent<Renderer>().material.color = Color.green;
        //        break;

        //    case TargetSize.Medium:
        //        mySpeed = 2f;
        //        myHealth = 300f;
        //        scaleFactor = 1;
        //        transform.localScale = Vector3.one * scaleFactor;
        //        transform.GetComponent<Renderer>().material.color = Color.yellow;
        //        break;

        //    case TargetSize.Small:
        //        mySpeed = 3f;
        //        myHealth = 100f;
        //        scaleFactor = 0.5f;
        //        transform.localScale = Vector3.one * scaleFactor;
        //        transform.GetComponent<Renderer>().material.color = Color.red;
        //        break;

        //}
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _TM.GetRandomSpawnPoint();
        endPos = TargetManager.instance.GetRandomSpawnPoint();
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
            TakeDamage(_FP.bulletDamage);
            Debug.Log(name + "has" + myHealth + "left");
        }

        if (collision.collider.CompareTag("Cannon"))
        {
            TakeDamage(_FP.cannonDamage);
            Debug.Log(name + "has" + myHealth + "left");
        }

        if (collision.collider.CompareTag("Pelt"))
        {
            TakeDamage(_FP.peltDamage);
            Debug.Log(name + "has" + myHealth + "left");
        }

        if (myHealth <= 0)
        {
            Die();
            Destroy(this.gameObject);
            Debug.Log("IM DED");
        }

    }

    public void Die()
    {
        StopAllCoroutines();
        _GM.OnTargetDie();
        _TM.KillTarget(this.gameObject);
    }

    public void Update()
    {
        //if (difficulty == Difficulty.Easy)
        //{
        //    ChangeSizeSmall();
        //}

        //if (difficulty == Difficulty.Medium)
        //{
        //    ChangeSizeMedium();
        //}

        //if (difficulty == Difficulty.Hard)
        //{
        //    ChangeSizeLarge();
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {
            difficulty = Difficulty.Easy;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            difficulty = Difficulty.Medium;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            difficulty = Difficulty.Hard;
        }
    }


    //public void ChangeSizeSmall()
    //{
    //    mySpeed = 3f;
    //    myHealth = 100f;
    //    scaleFactor = 0.5f;
    //    transform.localScale = Vector3.one * scaleFactor;
    //    transform.GetComponent<Renderer>().material.color = Color.red;
    //}

    //public void ChangeSizeMedium()
    //{
    //    mySpeed = 2f;
    //    myHealth = 300f;
    //    scaleFactor = 1;
    //    transform.localScale = Vector3.one * scaleFactor;
    //    transform.GetComponent<Renderer>().material.color = Color.yellow;
    //}

    //public void ChangeSizeLarge()
    //{
    //    mySpeed = 1f;
    //    myHealth = 500f;
    //    scaleFactor = 2;
    //    transform.localScale = Vector3.one * scaleFactor;
    //    transform.GetComponent<Renderer>().material.color = Color.green;
    //}

    public void TakeDamage(float _damage)
    {
        myHealth = myHealth - _damage;
    }


    //This fucntion worked perfectly before I added the UI and difficulty
    //Before I could manually change the size and it would work, the weapon damage worked as intended, reducing their HP accodringly and destroying when hp = 0
    //After I added UI and title it broke, and I tried to fix it for a whole day and a half and cant fix it.

}
