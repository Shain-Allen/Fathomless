using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class patrolScript : MonoBehaviour
{
    public GameObject[] pos;
    public int currentPos;

    public NavMeshAgent agent;
    public float speed;

    public GameObject player;
    public GameManager gManager;

    public float waitTime;
    public float startWaitTime;

    float distance;
    float patrolDistance;

    public bool canAttack;
    public bool canMove;

    public int patrolCase;

    public bool canFollowPlayer;
    public bool randomPatrol;

    public GameObject fish;

    public float fixedRotation;
    public bool takenDamage;
    public bool eyeDamage;
    public float timerTime;

    public patrolEnemyAttack attackScript;

    // Start is called before the first frame update
    void Start()
    {
        //pos = GameObject.FindGameObjectsWithTag("patrolPoint");
        patrolCase = 1;

        player = PlayerScript.Instance.gameObject;
        gManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;

        Debug.Log(patrolCase);
        distance = Vector3.Distance(transform.position, player.transform.position);


        if(distance >= 25)
        {
            patrolCase = 1;
        }

        if (canFollowPlayer)
        {
            if (distance <= 20 && distance >= 13)
            {
                patrolCase = 2;
            }

            if(distance <= 12)
            {
                patrolCase = 3;
            }
            if (distance <= 5)
            {
            }
        }

        if (eyeDamage)//figure out how to make him not look at the player at another time that isnt now.
        {
            patrolCase = 5;
            takenDamage = false;
            StartCoroutine(runAway());
        }

        /*if (takenDamage)
        {
            patrolCase = 2;
        }*/



        switch (patrolCase)
        {
            case 1:
                canMove = true;
                attackScript.doDamage = false;
                speed = 1f;
                transform.LookAt(pos[currentPos].transform.position);
                Vector3 eulerAngles = transform.eulerAngles;
                transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
                Patrol();
                break;
            case 2:
                canMove = false;
                attackScript.doDamage = false;

                agent.speed = 10f;
                transform.LookAt(player.transform.position);
                agent.SetDestination(player.transform.position);
                agent.isStopped = false;
                break;
            case 3:
                transform.LookAt(player.transform.position);
                canMove = false;
                agent.isStopped = true;
                attackScript.doDamage = true;
                //do damage here
                break;
            case 4:
                /*DamagePlayer(damageAmount);
                //play a damage animation here;
                eyeDamage = true;*/
                break;
            case 5:
                patrolDistance = Vector3.Distance(transform.position, pos[currentPos].transform.position);
                canMove = true;
                attackScript.doDamage = false;
                transform.LookAt(pos[currentPos].transform.position);
                agent.SetDestination(pos[currentPos].transform.position);
                speed = 15f;
                agent.isStopped = false;

                if (patrolDistance <= 5) 
                {
                    patrolCase = 1;
                    eyeDamage = false;   
                }
                break;
        }
    }

    public void Patrol()
    {
        if (canMove == true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, pos[currentPos].position, speed * Time.deltaTime);
            agent.SetDestination(pos[currentPos].transform.position);
            //agent.speed = 5f;
            //transform.LookAt(pos[currentPos].transform.position);
            if (Vector3.Distance(transform.position, pos[currentPos].transform.position) < 2f)
            {
                if (waitTime <= 0)
                {
                    

                    if(randomPatrol)
                    {
                        RandomPos();
                    }
                    else
                    {
                        NextPos();
                    }

                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;

                }
            }
        }

    }

    public void NextPos()
    {

        if (currentPos >= pos.Length-1)
        {
            currentPos = 0;
        }
        else
        {
            currentPos++;
        }
    }

    public void RandomPos()
    {
        currentPos = Random.Range(0, pos.Length);
    }

    public void DamagePlayer(float damage)
    {
        gManager.playerHealth -= damage;
    }

    IEnumerator runAway()
    {
        transform.LookAt(pos[currentPos].transform.position);
        yield return new WaitForSeconds(timerTime);
        eyeDamage = false;
    }
}
