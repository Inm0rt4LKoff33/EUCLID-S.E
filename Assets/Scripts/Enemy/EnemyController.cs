using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Alien States
    enum AlienStates
    {
        Wandering,
        Chasing,
        Attacking
    }

    // Position that the Alien will try to reach
    [Header("Target")]
    [SerializeField]
    Transform target;
    // Mask layer to detect the player
    [SerializeField]
    LayerMask playerMask;

    [Header("Wandering")]
    [SerializeField]
    LayerMask groundMask;

    // Variables for moving
    [SerializeField]
    float walkPointRange;
    [SerializeField]
    float sightRange;

    [Header("Attack")]
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackRate = 0.5F;
    // Flags
    bool isWalkPointSet;
    bool isAttacking;
    bool isPlayerOnRange;
    bool isTargetInAttackRange;

    //Animator
    [SerializeField]
    Animator animator;

    AlienStates currentState;
    // The NavMeshAgent allows the gameobject to move around an specific delimited plane
    NavMeshAgent agent;
    Vector3 walkPoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        isPlayerOnRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        isTargetInAttackRange = Physics.CheckSphere(transform.position, attackRate, playerMask);

        currentState = isPlayerOnRange && isTargetInAttackRange ? AlienStates.Attacking :
            isPlayerOnRange && !isTargetInAttackRange ? AlienStates.Chasing :
            AlienStates.Wandering;

        switch (currentState)
        {
            case AlienStates.Attacking:
                HandleAttack();
                break;
            case AlienStates.Chasing:
                HandleChase();
                break;
            case AlienStates.Wandering:
                OnWandering();
                break;
        }
    }

    private void HandleChase()
    {
        agent.SetDestination(target.position);
        animator.SetBool("isCrawling", true);
    }

    private void HandleAttack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isCrawling", true);

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

    private void OnWandering()
    {
        if (!isWalkPointSet)
        {
            FindNextWalkPoint();
            if (isWalkPointSet)
            {
                agent.SetDestination(walkPoint);
                animator.SetBool("isCrawling", true);
            }
            else 
            {
                animator.SetBool("isCrawling", false);
            }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
