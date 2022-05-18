using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy2 : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Transform player;
    [SerializeField] Transform[] waypoints;
    [SerializeField] int waypointIndex = 0;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    private float health = 100;
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] ParticleSystem hitParticle;

    // Bow Attributes 
    public GameObject bowProjectile;
    public float projectileSpeed = 30f;

    private float enemyDamage;
    private float playerDamage;

    public bool attack;

    // Patterns
    [SerializeField] Vector3 target;

    // Attaques
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // Etats
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            animator.SetBool("attacking", false);
            animator.SetBool("running", true);
            if (Vector3.Distance(agent.transform.position, target) < 1)
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
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

    private void ChasePlayer()
    {
        animator.SetBool("attacking", false);
        animator.SetBool("running", true);
        agent.SetDestination(player.position);
    }

    private void Stop()
    {
        agent.SetDestination(this.transform.position);
        animator.SetBool("running", false);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        animator.SetBool("attacking", true);

        if (!alreadyAttacked)
        {
            int rotationPos = (int)transform.localEulerAngles.y;
            if (rotationPos > 180)
            {
                rotationPos -= 360;
            }

            Rigidbody rb = Instantiate(bowProjectile, transform.position, Quaternion.Euler(0, rotationPos + 90, 90)).GetComponent<Rigidbody>();
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

    public void TakeDamage(float damage)
    {
        healthBarExtern.UpdateHealth(this.health / 100);
        this.health -= damage;
        if (this.health <= 0)
            DestroyEnemy();
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
    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Sword":
                TakeDamage(20f);
                break;
            case "Bullet":
                TakeDamage(40f);
                break;
            case "Axe":
                TakeDamage(40f);
                break;
            case "Arrow":
                TakeDamage(50f);
                break;
        }
    }
    
}
