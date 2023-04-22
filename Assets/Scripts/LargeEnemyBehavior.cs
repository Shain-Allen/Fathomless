using System.Collections;
using UnityEngine;

public class LargeEnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    Vector3 direction;
    [Range(0, 100)]
    public float swimSpeed;
    public Rigidbody rb;
    [Range(0, 100)]
    public float noiseSpeed;
    [Range(0, 100)]
    public float noiseScale;
    public float InitialAttackdelay = 5f;
    public float CurrentDelayTime;
    Material mat;
    public float HaltedSpeed = 40f;
    public float HaltTime = 1.5f;
    public float fleeSpeed = 100;

    public float enemyHealth = 100;

    public State currentState;
    public bool isAttackTimerRunning;

    public enum State
    {
        Idle, Pursue, Attack, Halt, Flee
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = State.Pursue;
        isAttackTimerRunning = false;

        //gets material so we can change the color during the attack state. Makes it look all twitchy and gross. I dont know why. :(
        mat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:

                break;
            case State.Pursue:
                HandlePursueState();
                break;
            case State.Attack:
                HandleAttackState();
                break;
            case State.Halt:
                HandleHaltState();
                break;
            case State.Flee:
                HandleFleeState();
                break;
            default:
                break;
        }

    }

    private void HandlePursueState()
    {
        //calculate to player vector, measure distance
        Vector3 toPlayer = Player.transform.position - transform.position;
        float dist = toPlayer.magnitude;

        //speed is based off of distance from player. Creates equilibrium, as this is when the player has time to fire at the enemy
        swimSpeed = toPlayer.magnitude;
        //adds noise for more organic movement
        Vector3 noise = new(Mathf.PerlinNoise(Time.time * noiseSpeed, 0), Mathf.PerlinNoise(0, Time.time * noiseSpeed), Mathf.PerlinNoise(Time.time * noiseSpeed, Time.time * noiseSpeed));
        noise -= Vector3.one * 0.5f;
        noise *= noiseScale;
        noise *= toPlayer.magnitude;

        //normalize, then apply to velocity
        toPlayer.Normalize();
        direction = toPlayer * swimSpeed + noise;
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);

        if (enemyHealth <= 0)
        {
            currentState = State.Flee;
        }

        //if monster enters equilibrium state, start timer for eventual attack
        //105 seemed to be the average equilibrium distance, at this speed.
        if (dist < 105 && isAttackTimerRunning == false)
        {
            StartCoroutine(StartAttackTimer());
        }
    }

    private void HandleAttackState()
    {
        //change color to attack color
        mat.color = Color.red;

        //calculate to player vector, and measure distance
        Vector3 toPlayer = Player.transform.position - transform.position;
        float dist = toPlayer.magnitude;

        //swimspeed is 20 as flat value, rather than being dependant on distance.
        swimSpeed = 200;
        //adds noise for more organic movement
        Vector3 noise = new(Mathf.PerlinNoise(Time.time * noiseSpeed, 0), Mathf.PerlinNoise(0, Time.time * noiseSpeed), Mathf.PerlinNoise(Time.time * noiseSpeed, Time.time * noiseSpeed));
        noise -= Vector3.one * 0.5f;
        noise *= noiseScale;
        noise *= toPlayer.magnitude;

        //normalize player vector
        toPlayer.Normalize();
        direction = toPlayer * swimSpeed + noise;
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);

        if (enemyHealth <= 0)
        {
            currentState = State.Flee;
        }

        if (dist < 10)
        {
            //Attack player. needs sub to take damage
            currentState = State.Halt;
            StartCoroutine(StartHaltTimer());
        }
    }

    private void HandleHaltState()
    {
        //calculate to player vector, measure distance
        Vector3 toPlayer = Player.transform.position - transform.position;

        //adds noise for more organic movement
        Vector3 noise = new(Mathf.PerlinNoise(Time.time * noiseSpeed, 0), Mathf.PerlinNoise(0, Time.time * noiseSpeed), Mathf.PerlinNoise(Time.time * noiseSpeed, Time.time * noiseSpeed));
        noise -= Vector3.one * 0.5f;
        noise *= noiseScale;
        noise *= toPlayer.magnitude;

        //normalize player vector
        toPlayer.Normalize();
        //Using HaltedSpeed, which will be significantly slower to allow player to create distance, while still moving the enemy.
        direction = toPlayer * HaltedSpeed + (noise / 5);
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);
    }

    private void HandleFleeState()
    {
        // calculate the direction away from the player
        Vector3 awayFromPlayer = transform.position - Player.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(awayFromPlayer);

        // smoothly rotate towards the target rotation
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * fleeSpeed);
        direction = transform.forward * swimSpeed;
        rb.velocity = direction;
    }

    //TODO: add animation for attack, add noise?
    private IEnumerator StartAttackTimer()
    {
        isAttackTimerRunning = true;
        print("AttackTimer Started");
        InitialAttackdelay = 10f;
        CurrentDelayTime = InitialAttackdelay;
        //yield return new WaitForSeconds() would work here, but with this system we can add to the timer if we want to. More flexible.
        while (CurrentDelayTime > 0)
        {
            if (enemyHealth <= 0)
            {
                break;
            }
            yield return null;
            CurrentDelayTime -= Time.deltaTime;
        }
        Debug.Log("attack state activated");
        currentState = State.Attack;
        isAttackTimerRunning = false;
    }

    //after attacking, hold for player to gain distance.
    private IEnumerator StartHaltTimer()
    {
        mat.color = Color.white;
        print("HaltTimer Started");
        yield return new WaitForSeconds(HaltTime);
        print("HaltTimer ended, pursue state active");
        currentState = State.Pursue;
    }
}
