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
    public WanderingState Wandering;
    public ChasingState Chasing;
    // Flags to change states
    bool isWandering;
    bool isRotatingLeft = false;
    bool isRotatingRight = false;
    bool isWalking = false;

    float moveSpeed = 3F;
    float rotationSpeed = 100F;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Wandering = new WanderingState();
        Chasing = new ChasingState();
    }

    void Start()
    {
        currentState = Wandering;
        isWandering = false;
        //if (isWandering) currentState.EnterState(this);
    }

    void Update()
    {
        if (!isWandering) StartCoroutine(Movement());
        if (isRotatingRight) transform.Rotate(transform.up * Time.deltaTime * rotationSpeed); 
        if (isRotatingLeft) transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        if (isWalking) transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public IEnumerator Movement()
    {
        int rotationTime = Random.Range(1, 3);
        int rotationWait = Random.Range(1, 4);
        int rotationLR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;
        yield return new WaitForSeconds(walkWait);

        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        
        isWalking = true;
        yield return new WaitForSeconds(rotationWait);

        if (rotationLR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }else
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;

        }
        isWandering = false; 
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
