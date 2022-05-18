using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private Transform player;
    [SerializeField] Transform startPosition;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    // [SerializeField] float health;

    public bool attack;

    // Patterns
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    // Attaques
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // Etats
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool patroling, fixedPos;

    // Animation
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

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
        }
       
        else
        {
            if (fixedPos)
            {
                ReturnStartPos();
                if (transform.position.x == startPosition.position.x 
                    && transform.position.z == startPosition.position.z)
                {
                    animator.SetBool("running", false);
                }
            }

            else
            {
                Patroling();
            }
        }
    }

    
    private void Patroling ()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }
    

    private void SearchWalkPoint ()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer ()
    {
        agent.SetDestination(player.position);
        animator.SetBool("running", true);
        animator.SetBool("attacking", false);
    }

    private void Stop()
    {
        agent.SetDestination(this.transform.position);
        animator.SetBool("running", false);
        animator.SetBool("attacking", true);
    }

    private void ReturnStartPos ()
    {
        animator.SetBool("running", true);
        animator.SetBool("attacking", false);
        agent.SetDestination(startPosition.position);
    }

    public void DestroyEnemy ()
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
