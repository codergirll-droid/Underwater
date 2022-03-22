using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    public float enemyHealth = 100f;
    public float enemyDamage = 20f;

    Transform Player;

    Transform enemyRot;
   
    LayerMask playerLayerMask;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    public float walkPointRange;
    bool walkPointSet;
    bool canMoveToWalkpoint = true;
    public float patrollingSpeed;

    [Header("Chasing Player")]
    public bool canChasePlayer;
    public float chaseSpeed;
    public float turnSpeed;

    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("General Bools")]
    public float sightRange, attackRange, runawayRange;
    public bool playerInSightRange, playerInAttackRange, playerInRunawayRange;

    Rigidbody rb;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayerMask = LayerMask.GetMask("playerLayerMask");
    }
    private void Start()
    {
        enemyRot = transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);
        playerInRunawayRange = Physics.CheckSphere(transform.position, runawayRange, playerLayerMask);
    
    
        if(playerInRunawayRange) 
        {
            canChasePlayer = false;
            canMoveToWalkpoint = false;
            RunningAway(); 
        }
        else if(playerInAttackRange)
        {
            canChasePlayer = false;

            canMoveToWalkpoint = false;
            AttackingPlayer(); 
        }
        else if(playerInSightRange) 
        {
            canChasePlayer = true;

            canMoveToWalkpoint = false;
            ChasingPlayer(); 
        }
        else
        {
            canChasePlayer = false;

            canMoveToWalkpoint = true;
            Patrolling(); 
        }
    
    }


    void Patrolling()
    {

        if (canMoveToWalkpoint == true)
        {
            if (walkPointSet == false)
            {
                SearchWalkPoint();
            }


            Quaternion lookOnLook = Quaternion.LookRotation(transform.position - walkPoint);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);

            transform.position = Vector3.MoveTowards(transform.position, walkPoint, patrollingSpeed * Time.deltaTime);


            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1)
            {
                Debug.Log("walkpointset is false");
                walkPointSet = false;
            }
        }
        backToNormalRotation(); 





    }

    void ChasingPlayer() 
    {
        if (canChasePlayer == true)
        {
            Vector3 playerPosition = Player.position;


            Quaternion lookOnLook = Quaternion.LookRotation(transform.position - playerPosition);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);

            transform.position = Vector3.MoveTowards(transform.position, playerPosition, chaseSpeed * Time.deltaTime);

            Vector3 distanceToWalkPoint = transform.position - playerPosition;

            if (distanceToWalkPoint.magnitude <= attackRange)
            {
                canChasePlayer = false;
            }
        }
    }

    void AttackingPlayer() 
    {
        Vector3 playerPosition = Player.position;
        Quaternion lookOnLook = Quaternion.LookRotation(transform.position - playerPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);
    }

    void RunningAway() { }



    void SearchWalkPoint()
    {
        Debug.Log("Searching for walkpoint");

        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, randomY, randomZ);

        Debug.Log("walkpoint set is true");
        walkPointSet = true;



    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
        if (!playerInRunawayRange && !playerInAttackRange && !playerInSightRange)
        {
            walkPointSet = false;
        }
    }

    void backToNormalRotation()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(transform.position - enemyRot.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Color x = Gizmos.color;
        x.a = 0.7f;
        Gizmos.color = x;
        Gizmos.DrawSphere(transform.position, runawayRange);
        Gizmos.color = Color.red;

        x = Gizmos.color;
        x.a = 0.5f;
        Gizmos.color = x;
        Gizmos.DrawSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        x = Gizmos.color;
        x.a = 0.2f;
        Gizmos.color = x;
        Gizmos.DrawSphere(transform.position, sightRange);
    }


}
