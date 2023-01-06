using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    public GameObject zombie;
   
    private NavMeshAgent agent;
    private Animator anim;

    private GameObject player;
    public LayerMask whatIsPlayer;
    public float health = 50f;

    // Attacking
    public float damage = 10f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    
    // Respawn values
    float startHealth;
    bool isDying;
    
    [Header("Minimap Sprites")]
    private GameObject minimapSymbol;


    void Awake()
    {
        player = GameObject.Find("PlayerObject");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //startPosition = gameObject.transform.position;
        startHealth = health;
        isDying = false;

        minimapSymbol = transform.GetChild(0).gameObject;
        minimapSymbol.SetActive(true);

        // Ragdoll components
        SetRigidbodyState(true);
    }



    void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked && !isDying) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !isDying) AttackPlayer();

        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }


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
           
            // Play audio clip
            GetComponent<EnemySounds>().HitSound();

            anim.SetBool("Attacking", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks - 1f);
        }

    }

    public void DoAttack()
    {
        // Triggered within the enemy attack animation, so player only takes damage when the blow actually hits
        if(playerInAttackRange && !isDying)
            PlayerStats.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("Attacking", false);
    }


    public void TakePhysicalDamage(float damageAmount)
    {
        health -= damageAmount;
        GetComponent<EnemySounds>().MaceHitSound();


        if (health <= 0 && !isDying)
        {
            agent.SetDestination(gameObject.transform.position); // So enemy don't continue to move while dying animation

            // Add to kill score
            if(!isDying)
                PlayerStats.instance.AddToStat("kill", 1);
            isDying = true;
    
            Die();
            
        }
    }

    private void Die()
    {

        // Ragdoll components
        GetComponent<Animator>().enabled = false;
        minimapSymbol.SetActive(false);
        GetComponent<EnemySounds>().DieSound();
        SetRigidbodyState(false);

        // Time before they respawn
        Invoke(nameof(Respawn), 3f);

    }



    private void Respawn()
    {
        minimapSymbol.SetActive(true);
        GetComponent<Animator>().enabled = true;
        SetRigidbodyState(true);

        startHealth += 5;
        health = startHealth;
        isDying = false;
        
        zombie.SetActive(false);
    }

    void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

}
