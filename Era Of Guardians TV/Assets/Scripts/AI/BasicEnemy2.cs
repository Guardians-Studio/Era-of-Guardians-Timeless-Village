using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy2 : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Transform player;
    [SerializeField] Transform[] waypoints;
    [SerializeField] int waypointIndex;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] float health;
    // Bow Attributes 


    public GameObject bowProjectileSpawnPoint;
    public GameObject bowProjectile;
    public AudioClip bowAttack;
    public float damage = 40f;
    public float projectileSpeed = 30f;
    public float cooldown = 2.5f;


    public bool attack;

    // Patterns
    [SerializeField] Vector3 target;

    // Attaques
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // Etats
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update()
    {
        if (GameObject.FindWithTag("Player"))
            player = GameObject.FindWithTag("Player").transform;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange && playerInSightRange)
        {
            attack = false;
            ChasePlayer();
        }

        else if (playerInAttackRange && playerInSightRange)
        {
            attack = true;
            Stop();
            AttackPlayer();
        }

        else
        {
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
            else
                agent.SetDestination(target);
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    private void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Stop()
    {
        agent.SetDestination(this.transform.position);
    }

    private void AttackPlayer()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            Rigidbody rb = Instantiate(bowProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Invoke(nameof(DestroyEnemy), 0.5f);
    }
    
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
