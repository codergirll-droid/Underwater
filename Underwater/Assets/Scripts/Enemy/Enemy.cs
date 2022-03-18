using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    public float enemyHealth = 100f;
    public float enemyDamage = 20f;

    Transform Player;
   
    LayerMask playerLayerMask;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    public float walkPointRange;
    bool walkPointSet;
    public float walkTime;

    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("General Bools")]
    public float sightRange, attackRange, runawayRange;
    public bool playerInSightRange, playerInAttackRange, playerInRunawayRange;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayerMask = LayerMask.GetMask("playerLayerMask");
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);
        playerInRunawayRange = Physics.CheckSphere(transform.position, runawayRange, playerLayerMask);
    
    
        if(playerInRunawayRange) { RunningAway(); }
        else if(playerInAttackRange) { AttackingPlayer(); }
        else if(playerInSightRange) { ChasingPlayer(); }
        else { Patrolling(); }
    
    }


    void Patrolling()
    {      
        if (!walkPointSet) SearchWalkPoint();
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    
    }

    void ChasingPlayer() { }

    void AttackingPlayer() { }

    void RunningAway() { }


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

    void SearchWalkPoint()
    {

        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, randomY, randomZ);

        walkPointSet = true;

        transform.DOMove(walkPoint, walkTime);


    }


}
