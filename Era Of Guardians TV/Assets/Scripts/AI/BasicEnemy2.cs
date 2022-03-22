using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy2 : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    private Transform player;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;

    [SerializeField] float timeBetweenAttacks;

    public bool attack;

    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] Transform aller, retour;

    private void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
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
            Patroling();
        }
        
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            AllerRetour();
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
    

    private void AllerRetour ()
    {
        if (agent.SetDestination(aller.position))
            agent.SetDestination(retour.position);
        else
            agent.SetDestination(aller.position);
    }

    private void ChasePlayer ()
    {
        agent.SetDestination(player.position);
    }

    private void Stop ()
    {
        agent.SetDestination(this.transform.position);
    }

    


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}