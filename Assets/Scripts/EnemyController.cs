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
    float timer;
    // The NavMeshAgent allows the gameobject to move around an specific delimited plane
    public NavMeshAgent agent;

    public EnemyBaseState currentState;
    public WanderingState Wandering = new WanderingState();
    public ChasingState Chasing = new ChasingState();
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState = Chasing;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
