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

    public GameObject bowProjectile, sword;
    public float projectileSpeed = 30f;

    [Header("Sword")]
    // Animator anim;
    private bool animation = true;

    private float enemyDamage;
    private float playerDamage;

    public bool attack;

    private float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    private float sightRange = 20f;
    private float attackRange = 2f;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        TakeDamage(0f);
        animator = GetComponent<Animator>();
        animator.SetBool("running", false);
        animator.SetBool("shooting", false);
        animator.SetBool("attacking", false);
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
            if (this.health <= 50)
            {
                AttackPlayer2();
            }
            else
            {
                AttackPlayer1();
            }
        }
    }

    public void TakeDamage(float damage)
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
        animator.SetBool("running", true);
        animator.SetBool("shooting", false);
        animator.SetBool("attacking", false);
        agent.SetDestination(player.position);
    }

    private void Stop()
    {
        animator.SetBool("running", false);
        agent.SetDestination(this.transform.position);
    }

    private void AttackPlayer1()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("shooting", false);
        animator.SetBool("attacking", true);

        if (!alreadyAttacked)
        {
            int rotationPos = (int)transform.localEulerAngles.y;
            if (rotationPos > 180)
            {
                rotationPos -= 360;
            }
            Rigidbody rb = Instantiate(sword, transform.position, Quaternion.Euler(0, rotationPos + 90, 90)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            if (animation)
            {
                // anim = sword.GetComponentInChildren<Animator>();
                // anim.SetTrigger("Attack");
            }
        }
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(4);
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
            if (animation)
            {
                // anim = sword.GetComponentInChildren<Animator>();
                // anim.SetTrigger("Attack");
            }
        }
    }

    private void AttackPlayer2 ()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        timeBetweenAttacks = 5f;
        attackRange = 10f;
        animation = false;
        animator.SetBool("shooting", true);
        animator.SetBool("attacking", false);

        StartCoroutine(Shooting());
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
