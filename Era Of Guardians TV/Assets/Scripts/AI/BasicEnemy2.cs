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
    public bool patroling, fixedPos;

    public float startX, startZ, endX, endZ;


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

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer)

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
            FixedPatrol();
        }
    }

    private void FixedPatrol ()
    {

        if (!walkPointSet)
        {
            Aller();
            Retour();
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

    private void Aller ()
    {
        walkPoint = new Vector3(transform.position.x + endX, transform.position.y, transform.position.z + endZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Retour ()
    {
        walkPoint = new Vector3(transform.position.x + startX, transform.position.y, transform.position.z + startZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }


    private void ChasePlayer ()
    {
        agent.SetDestination(player.position);
    }

    private void Stop ()
    {
        agent.SetDestination(this.transform.position);
    }

    private void ReturnStartPos ()
    {
        agent.SetDestination(startPosition.position);
    }

    private void OnDrawGizmosSelected()
    {
        BasicEnemy.OnDrawGizmosSelected();
    }
}