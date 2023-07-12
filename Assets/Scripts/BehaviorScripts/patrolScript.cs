using UnityEngine;
using UnityEngine.AI;

public class patrolScript : MonoBehaviour
{
    public GameObject[] pos;
    public int currentPos;

    public NavMeshAgent agent;
    public float speed;

    public GameObject player;

    public float waitTime;
    public float startWaitTime;

    float distance;
    float patrolDistance;

    public bool canAttack;
    public bool canMove;

    int patrolCase;

    public bool canFollowPlayer;
    public bool randomPatrol;

    public GameObject fish;

    public float fixedRotation;
    public bool takenDamage;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.FindGameObjectsWithTag("patrolPoint");
        patrolCase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;

        Debug.Log(patrolCase);
        distance = Vector3.Distance(transform.position, player.transform.position);


        if(distance >= 20)
        {
            patrolCase = 1;
        }

        if (canFollowPlayer)
        {
            if (distance <= 15 && distance >= 8)
            {
                patrolCase = 2;
            }

            if(distance <= 7)
            {
                patrolCase = 3;
            }
            if (distance <= 2)
            {
                patrolCase = 4;
            }
        }

        if(takenDamage)
        {
            patrolCase = 5;
        }

        switch (patrolCase)
        {
            case 1:
                canMove = true;
                speed = 1f;
                transform.LookAt(pos[currentPos].transform.position);
                Patrol();
                break;
            case 2:
                canMove = false;
                agent.speed = 10f;
                transform.LookAt(player.transform.position);
                agent.SetDestination(player.transform.position);
                agent.isStopped = false;
                break;
            case 3:
                transform.LookAt(player.transform.position);
                canMove = false;
                agent.isStopped = true;
                break;
            case 4:

                break;
            case 5:
                patrolDistance = Vector3.Distance(transform.position, pos[currentPos].transform.position);
                canMove = true;
                transform.LookAt(pos[currentPos].transform.position);
                agent.SetDestination(pos[currentPos].transform.position);
                speed = 15f;
                agent.isStopped = false;

                if (patrolDistance <= 5) 
                {
                    patrolCase = 1;
                    takenDamage = false;   
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
}
