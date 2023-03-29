using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Position that the Alien will try to reach
    [SerializeField]
    public Transform target;
    // Intervals of times, how often the Alien will go after the Player
    [SerializeField]
    public float interval;
    // *** The time limit, stores the game runtime from a specific moment
    public float timer;
    // The NavMeshAgent allows the gameobject to move around an specific delimited plane
    public NavMeshAgent agent;
    // States Machine Interface
    public EnemyBaseState currentState;
    // States Classes
    public Wander Wander;
    public ChasingState Chasing;

    public float moveSpeed;
    public float rotationSpeed;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander = new Wander();
        Chasing = new ChasingState();
    }

    void Start()
    {
        currentState = Wander;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
