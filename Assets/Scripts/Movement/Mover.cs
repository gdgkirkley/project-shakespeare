
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float stoppingDistance = 0.2f;

    NavMeshAgent navMeshAgent;
    // Health health;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // health = GetComponent<Health>();
    }

    void Update()
    {
        // Disable the navmesh if the character is dead.
        // navMeshAgent.enabled = !health.isDead();
        UpdateAnimator();
    }

    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination, speedFraction);
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped = false;
    }

    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }

    private void UpdateAnimator()
    {
        Animator animator = GetComponent<Animator>();

        // If the agent has reached the destination, set to 0.
        if (navMeshAgent.remainingDistance < stoppingDistance)
        {
            animator.SetFloat("forwardSpeed", 0);
            return;
        }

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;

        animator.SetFloat("forwardSpeed", speed);
    }
}
