using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
   
    public NavMeshAgent agent;

    
    

    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 50f;

    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float damage = 10f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    // Respawn values
    Vector3 startPosition;
    float startHealth;


    void Awake()
    {
        player = GameObject.Find("PlayerObject");
        agent = GetComponent<NavMeshAgent>();
        startPosition = gameObject.transform.position;
        startHealth = health;

    }


    void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        

        
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Check if walkpoint is reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        // Finde random coordinates within range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if checkpoint is inside map
        if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround)) 
            walkPointSet = true;
        
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer() 
    {
        // Make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            // Attack code
            Debug.Log("Do Damage!");
            PlayerStats.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    public void TakePhysicalDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy dead --- Do something..");
        gameObject.transform.position = startPosition;
        health = startHealth;
    }
}
