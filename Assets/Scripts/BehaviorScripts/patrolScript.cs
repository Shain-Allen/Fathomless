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

    public bool canAttack;
    public bool canMove;

    int patrolCase;

    public bool canFollowPlayer;
    public bool randomPatrol;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.FindGameObjectsWithTag("patrolPoint");
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;

        Debug.Log(patrolCase);
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance >= 15)
        {
            patrolCase = 1;
        }

        if (canFollowPlayer)
        {
            if (distance <= 10 && distance >= 3)
            {
                patrolCase = 2;
            }

            if(distance <= 3)
            {
                patrolCase = 3;
            }
        }

        switch (patrolCase)
        {
            case 1:
                canMove = true;
                //transform.LookAt(pos[currentPos].transform.position);\
                Patrol();
                break;
            case 2:
                canMove = false;
                transform.LookAt(player.transform.position);
                agent.SetDestination(player.transform.position);
                agent.isStopped = false;
                break;
            case 3:
                transform.LookAt(player.transform.position);
                canMove = false;
                agent.isStopped = true;
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
