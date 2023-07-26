using System.Collections;
using UnityEngine;

public class LargeEnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    public GameObject damageHandler;
    public GameObject bloodEffect;
    Vector3 direction;
    [Range(0, 100)]
    public float swimSpeed;
    public Rigidbody rb;
    [Range(0, 5)]
    public float noiseSpeed;
    [Range(0, 5)]
    public float noiseScale;
    public float InitialAttackdelay = 5f;
    public float CurrentDelayTime;
    Material mat;
    public float HaltedSpeed = 40f;
    public float HaltTime = 1.5f;
    public float StunTime = 5;
    public float StunCooldown;
    public float fleeSpeed = 100;
    public float attackSwimSpeed;

    public bool eelFleeing;

    public float enemyHealth = 100;

    public State currentState;
    public bool isAttackTimerRunning;

    public enum State
    {
        Idle, Pursue, Attack, Halt, Flee, Stun
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = State.Pursue;
        isAttackTimerRunning = false;

        //gets material so we can change the color during the attack state. Makes it look all twitchy and gross. I dont know why. :(
        //mat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        
        if (eelFleeing)
        {
            currentState = State.Flee;
        }
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
            case State.Stun:
                HandleStunState();
                break;
            default:
                break;
        }

    }

    public void HarpoonHit(float HarpoonDamage)
    {
        enemyHealth -= HarpoonDamage;
    }

    private void HandlePursueState()
    {
        //calculate to player vector, measure distance
        Vector3 toPlayer = SubController.instance.chasePoint.transform.position - transform.position;
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
            eelFleeing = true;
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
        //mat.color = Color.red;

        //calculate to player vector, and measure distance
        Vector3 toPlayer = SubController.instance.chasePoint.transform.position - transform.position;
        float dist = toPlayer.magnitude;

        //swimspeed is 20 as flat value, rather than being dependant on distance.
        swimSpeed = attackSwimSpeed;
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
            eelFleeing = true;
        }

        if (dist < 10)
        {
            SubDamageManager.instance.Hit();
            currentState = State.Halt;
            StartCoroutine(StartHaltTimer());
        }
    }

    private void HandleHaltState()
    {
        //calculate to player vector, measure distance
        Vector3 toPlayer = SubController.instance.chasePoint.transform.position - transform.position;

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

    private void HandleStunState()
    {
        if (enemyHealth <= 0)
        {
            currentState = State.Flee;
            eelFleeing = true;
        }
        //calculate to player vector, measure distance
        Vector3 toPlayer = SubController.instance.chasePoint.transform.position - transform.position;

        // calculate the direction away from the player
        Vector3 awayFromPlayer = transform.position - SubController.instance.chasePoint.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(awayFromPlayer);

        // smoothly rotate towards the target rotation
        //rb.rotation = Quaternion.LookRotation(direction);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * (fleeSpeed * 2));
        direction = transform.forward * swimSpeed;
        rb.velocity = direction;
        StartCoroutine(StartStunTimer());
    }

    private void HandleFleeState()
    {
        if (enemyHealth <= 0)
        {
            //The eel has been slain, end demo. This shouldn't run with the first eel because it has a brazillian health
            StartCoroutine(LetEelFlee());
        }
        // calculate the direction away from the player
        Vector3 awayFromPlayer = transform.position - SubController.instance.chasePoint.transform.position;
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
        if (currentState != State.Stun && currentState != State.Halt)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Pursue;
        }
        isAttackTimerRunning = false;
    }

    //after attacking, hold for player to gain distance.
    private IEnumerator StartHaltTimer()
    {
        yield return new WaitForSeconds(HaltTime);
        currentState = State.Pursue;
    }
    private IEnumerator StartStunTimer()
    {
        yield return new WaitForSeconds(StunTime);
        currentState = State.Pursue;
    }
    private IEnumerator LetEelFlee()
    {
        yield return new WaitForSeconds(5);
        GameManager.gminstance.EndGame();
    }
}
