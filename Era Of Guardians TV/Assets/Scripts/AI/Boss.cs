using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    private float health = 100f;
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] ParticleSystem hitParticle;

    // Bow Attributes
    public GameObject bowProjectile;
    public float projectileSpeed = 30f;

    private float enemyDamage;
    private float playerDamage;

    public bool attack;

    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    private float sightRange = 20f;
    private float attackRange = 1f;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        TakeDamage(0f);
    }

    private void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }

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
    }

    private void TakeDamage(float damage)
    {
        healthBarExtern.UpdateHealth(this.health / 100);
        this.health -= damage;
        if (this.health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Stop()
    {
        agent.SetDestination(this.transform.position);
    }

    private void AttackPlayer ()
    {
        if (this.health <= 50)
        {
            attackRange = 10f;
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(bowProjectile, transform.position, Quaternion.Euler(0, agent.transform.localRotation.y, 90)).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
                TakeDamage(10f);
                break;
            case "Bullet":
                TakeDamage(20f);
                break;
            case "Axe":
                TakeDamage(20f);
                break;
            case "Arrow":
                TakeDamage(25f);
                break;
        }
    }
}
