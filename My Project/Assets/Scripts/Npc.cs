using UnityEngine;
using UnityEngine.AI;

public class SimplePatrolAI : MonoBehaviour
{
    public float patrolRadius = 10f;  // How far the NPC can roam from the start
    public float patrolDelay = 2f;    // Delay between moving to next point
    private NavMeshAgent agent;
    private Vector3 origin;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        timer = patrolDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // If the NPC has reached the destination and waited enough time
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (timer >= patrolDelay)
            {
                Vector3 newPos = RandomNavSphere(origin, patrolRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    // Helper function to pick a random point on the NavMesh
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);
        return navHit.position;
    }
}
