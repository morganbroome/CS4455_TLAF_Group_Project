using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    // Local components
    private NavMeshAgent agent;

    // For navigation path status
    private float stoppingDistance = 1f;
    private float maxLookAheadTime = 3f;
    private bool hasPath = false;

    // For static and moving waypoints
    private Vector3 targetPosition;

    // For debugging
    public bool debugFlag = false;
    public GameObject wp;

    // Waypoints
    public GameObject waypointParent;
    public List<GameObject> waypoints;
    public int currWaypoint;

    void Start()
    {
        Transform[] buffer = waypointParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in buffer)
        {
            if (child != buffer[0])
            {
                waypoints.Add(child.gameObject);
            }
        }
        wp = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        agent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    void Update()
    {
        if (GetComponent<EnemyStateManager>().state == EnemyStateManager.State.Chase)
        {
            // Get waypoint
            targetPosition = wp.transform.position;

            VelocityReporter target = wp.GetComponent<VelocityReporter>();
            VelocityReporter interceptor = this.GetComponent<VelocityReporter>();

            // Calculate distance and time to intercept
            float distance = Vector3.Distance(transform.position, targetPosition);
            float time = Mathf.Clamp(distance / interceptor.velocity.magnitude, 0, maxLookAheadTime);

            // Predict point of interception
            Vector3 predictedDistance = target.velocity * time;

            // Check if target is moving towards interceptor
            float dotProduct = Vector3.Dot(Vector3.Normalize(interceptor.direction), Vector3.Normalize(target.direction));
            if (dotProduct < 0)
            {
                // Projecting respective velocities
                Vector3 projection = Vector3.Project(interceptor.velocity, target.velocity);
                time = Mathf.Clamp(distance / (target.velocity.magnitude + interceptor.velocity.magnitude), 0, maxLookAheadTime);

                // Update pediction
                predictedDistance = target.velocity * time;
            }

            // Raycast to find bounds
            NavMeshHit hit;
            NavMesh.Raycast(this.transform.position, targetPosition, out hit, NavMesh.AllAreas);

            targetPosition += predictedDistance;

            /*
            targetPosition.x = Mathf.Clamp(targetPosition.x, wp.transform.position.x, hit.position.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, wp.transform.position.y, hit.position.y);
            targetPosition.z = Mathf.Clamp(targetPosition.z, wp.transform.position.z, hit.position.z);
            */

            // Set new target position
            agent.SetDestination(targetPosition);

            // Debugging
            if (debugFlag)
            {
                Debug.Log("Distance to point: " + distance + "\nMinion speed: " + "null" + "\nTime to point: " + time
                            + "\nWaypoint velocity: " + target.velocity + "\nWaypoint speed: " + target.velocity.magnitude
                            + "\nWaypoint direction: " + target.direction + "\nPredicted distance: " + predictedDistance + "\nTarget position: " + targetPosition
                            + "\nDot product: " + dotProduct);
            }

            if (AtEndOfPath() && agent.remainingDistance <= agent.stoppingDistance && Vector3.Distance(agent.transform.position, wp.transform.position) < 5)
            {
                Debug.Log("Player game over");
                wp.transform.position = new Vector3(0, 0.5f, 0);
                wp.transform.GetComponent<PlayerController>().playDeathSound();
            }


            //Debug.Log("Hit distance: " + hit.distance + "\nHit flag: " + hit.hit + "\nHit position: " + hit.position);
        }
        else
        {
            if (AtEndOfPath() && agent.remainingDistance <= agent.stoppingDistance)
            {
                SetNextWaypoint();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetPosition, 1f);
        Gizmos.DrawRay(targetPosition, this.transform.forward * Vector3.Distance(transform.position, targetPosition));
    }

    bool AtEndOfPath()
    {
        hasPath |= agent.hasPath;
        if (hasPath && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Arrived
            hasPath = false;
            return true;
        }
        return false;
    }
    private void SetNextWaypoint()
    {
        currWaypoint = (currWaypoint + 1) % waypoints.Count;
        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[currWaypoint].transform.position);
            targetPosition = waypoints[currWaypoint].transform.position;
        }
        else
        {
            Debug.LogError("There are no valid points in the waypoints array.");
        }

        // Adjust stopping distance
        agent.stoppingDistance = (waypoints[currWaypoint].GetComponent<VelocityReporter>()) ? 1f : 0.5f;
    }

    private Vector3 CClamp(Vector3 target, Vector3 reference, Vector3 max)
    {
        return target;
    }
}
