using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player, startPosition;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    // [SerializeField] float health;

    // Patterns
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    // Attaques
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    // Etats
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;
    [SerializeField] bool patroling, fixedPos;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startPosition = GameObject.Find("central_place_ground").transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); 
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && playerInSightRange)
        {
            ChasePlayer();
            print("yes");
        }
        /*
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        */
        else
        {
            if (fixedPos)
            {
                ReturnStartPos();

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
    }
    
    private void ReturnStartPos ()
    {
        agent.SetDestination(startPosition.position);
    }
   
    /*
    private void AttackPlayer ()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            /*
             * Code pour gerer les attaques des ennemis
             */
            
    /*
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    */

    /*
    private void ResetAttack ()
    {
        alreadyAttacked = false;
    }
    */


    /*
    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    */

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
