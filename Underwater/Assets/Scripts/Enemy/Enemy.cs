using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    public float enemyHealth = 100f;
    public float enemyDamage = 20f;

    Transform PlayerTransform;

    

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

    [Header("Runaway")]
    public bool canRunAway;
    public float runAwaySpeed;

    [Header("General Bools")]
    public float sightRange, attackRange, runawayRange;
    public bool playerInSightRange, playerInAttackRange, playerInRunawayRange;

    Rigidbody rb;

    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
            canRunAway = true;

            canChasePlayer = false;
            canMoveToWalkpoint = false;
            RunningAway(); 
        }
        else if(playerInAttackRange)
        {
            canRunAway = false;

            canChasePlayer = false;

            canMoveToWalkpoint = false;
            AttackingPlayer(); 
        }
        else if(playerInSightRange) 
        {
            canRunAway = false;

            canChasePlayer = true;

            canMoveToWalkpoint = false;
            ChasingPlayer(); 
        }
        else if(!playerInAttackRange && !playerInRunawayRange && !playerInSightRange)
        {
            canRunAway = false;

            canChasePlayer = false;

            canMoveToWalkpoint = true;
            Patrolling(); 
        }
    
    }



    #region MOVEMENT AND ATTACK

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
                walkPointSet = false;
            }
        }
        backToNormalRotation();

        



    }

    void ChasingPlayer()
    {
        Vector3 playerPosition = PlayerTransform.position;

        Vector3 distanceToWalkPoint = transform.position - playerPosition;
        Quaternion lookOnLook = Quaternion.LookRotation(transform.position - playerPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);

        if (canChasePlayer == true && distanceToWalkPoint.magnitude <= sightRange)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerPosition, chaseSpeed * Time.deltaTime);

        }
        else if (distanceToWalkPoint.magnitude > sightRange)
        {
            canChasePlayer = false;
        }
    }

    void AttackingPlayer()
    {

        Vector3 playerPosition = PlayerTransform.position;
        Quaternion lookOnLook = Quaternion.LookRotation(transform.position - playerPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);


        //!SHOOT THE PLAYER WITH RAYCASTS BETWEEN ATTACK TIMES
        shootBullet();
        
    }

    void RunningAway()
    {
        Vector3 playerPosition = PlayerTransform.position;
        Vector3 distanceToWalkPoint = transform.position - playerPosition;

        Quaternion lookOnLook;

        float runForMeters = runawayRange - distanceToWalkPoint.magnitude;

        Vector3 goTo = transform.position + new Vector3(runForMeters, 0, runForMeters);


        //! RUN THE OTHER WAY IF THE PLAYER GETS IN THE RUNNING AWAY AREA
        if (canRunAway == true && distanceToWalkPoint.magnitude < runawayRange)
        {
            lookOnLook = Quaternion.LookRotation(playerPosition - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);


            transform.position = Vector3.MoveTowards(transform.position, goTo, runAwaySpeed * Time.deltaTime);

        }
        else if (distanceToWalkPoint.magnitude > runawayRange)
        {
            canRunAway = false;

            lookOnLook = Quaternion.LookRotation(transform.position - playerPosition);


            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);

        }
    }



    void SearchWalkPoint()
    {

        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, randomY, randomZ);

        walkPointSet = true;



    }


    void backToNormalRotation()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(transform.position - enemyRot.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * turnSpeed);
    }



    void shootBullet()
    {

        if (!alreadyAttacked)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 30))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
            }

            alreadyAttacked = true;
            Player.Instance.takeDamage(5);


            Invoke(nameof(resetAttack), timeBetweenAttacks);

        }



    }

    void resetAttack()
    {
        alreadyAttacked = false;
    }

    #endregion


    #region COLLISION
    private void OnCollisionEnter(Collision collision)
    {
        if (!playerInRunawayRange && !playerInAttackRange && !playerInSightRange)
        {
            walkPointSet = false;
        }

    }

    #endregion

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

    #region HEALTH STATS

    public void takeDamage(int damageValue)
    {
        if (enemyHealth - damageValue > 0)
        {
            enemyHealth -= damageValue;
            //! SET ENEMY UI 
        }
        else
        {
            enemyHealth = 0;
            Destroy(this.gameObject);
        }

    }

    #endregion







}
