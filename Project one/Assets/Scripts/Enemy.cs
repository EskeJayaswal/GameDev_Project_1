using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
   
    private NavMeshAgent agent;
    private Animator anim;
    
    

    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 50f;

/*
    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    */

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
    bool isDying;


    void Awake()
    {
        player = GameObject.Find("PlayerObject");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        startPosition = gameObject.transform.position;
        startHealth = health;
        isDying = false;

    }


    void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked && !isDying) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !isDying) AttackPlayer();
        

        SetAnimationParameters();
    }

/*
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

*/

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        
    }

    private void AttackPlayer() 
    {
        // Make sure enemy doesnt move
        agent.SetDestination(transform.position);



        if (!alreadyAttacked)
        {
            transform.LookAt(player.transform);

            alreadyAttacked = true;
            PlayerStats.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            anim.SetTrigger("Attack");
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
            agent.SetDestination(gameObject.transform.position); // So enemy don't continue to move while dying animation

            isDying = true;
            anim.SetTrigger("Dying");
            Invoke(nameof(Die), 2.9f);

            //Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    
        gameObject.transform.position = startPosition;

        // Time before they respawn
        Invoke(nameof(Respawn), 2f);

    }

    private void Respawn()
    {
        anim.SetTrigger("Respawn");
        if (agent.speed <= 10){
            // Make the enemy 1 faster every time they respawn
            agent.speed += 1; 
        }
        startHealth += 5;
        health = startHealth;
        gameObject.SetActive(true);
        isDying = false;
    }

        private void SetAnimationParameters()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
