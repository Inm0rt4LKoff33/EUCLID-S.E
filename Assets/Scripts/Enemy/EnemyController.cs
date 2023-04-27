using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Alien States
    enum AlienStates
    {
        Patroling,
        Chasing,
        Attacking
    }

    [Header("Target")]
    [SerializeField]
    Transform target;

    [SerializeField]
    LayerMask playerMask;

    Animator animator;

    // Mask layer to detect the player

    [Header("Wandering")]
    [SerializeField]
    LayerMask groundMask;

    // Variables for moving
    [SerializeField]
    float walkPointRange = 20;
    [SerializeField]
    float sightRange = 5;

    [Header("Attack")]
    [SerializeField]
    float attackRange = 1;
    [SerializeField]
    float attackRate = 1;

    NavMeshAgent agent;
    Vector3 walkPoint;

    bool isWalkPointSet;
    bool isAttacking;
    bool isTargetOnSight;
    bool isTargetInAttackRange;

    AlienStates currentState;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isTargetOnSight = Physics.CheckSphere(transform.position, sightRange, playerMask);
        isTargetInAttackRange = Physics.CheckSphere(transform.position, attackRate, playerMask);

        currentState = isTargetOnSight && isTargetInAttackRange ? AlienStates.Attacking :
            isTargetOnSight && !isTargetInAttackRange ? AlienStates.Chasing :
            AlienStates.Patroling;

        switch (currentState)
        {
            case AlienStates.Attacking:
                HandleAttack();
                break;
            case AlienStates.Chasing:
                HandleChase();
                break;
            case AlienStates.Patroling:
                HandlePatrol();
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void HandleChase()
    {
        agent.SetDestination(target.position);
        animator.SetBool("isCrawling", true);
    }

    private void HandlePatrol()
    {
        if (!isWalkPointSet)
        {
            FindNextWalkPoint();
            if (isWalkPointSet)
            {
                agent.SetDestination(walkPoint);
                animator.SetBool("isCrawling", true);
            }
            else animator.SetBool("isCrawling", false);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1)
        {
            isWalkPointSet = false;
        }
    }

    private void FindNextWalkPoint()
    {
        float positionX = Random.Range(-walkPointRange, walkPointRange);
        float positionZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + positionX,
            transform.position.y,
            transform.position.z + positionZ);

        if (Physics.Raycast(walkPoint, -transform.up, groundMask))
        {
            isWalkPointSet = true;
        }
    }

    private void HandleAttack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isCrawling", false);
        transform.LookAt(target);

        if (!isAttacking)
        {
            isAttacking = true;
            Invoke(nameof(ResetAttack), attackRate);
            animator.SetBool("isAttacking", true);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}