using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EvilMrBeast : MonoBehaviour {
    public GameObject player;
    public NavMeshAgent agent;
    Vector3 offset;
    public LayerMask whatIsGround, whatIsPlayer;

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;
    private const string ISWALKING = "isWalking";
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource steps;
    [SerializeField] private AudioSource evilMusic;

    private GameManager gameManager;
    [SerializeField] private FirstPersonMovement playermovement;
 
    // States
    public float sightRange;
    public bool playerInSightRange;
    private float timeSinceLastSeenPlayer;
    public float timeToLosePlayer = 4f; // Time to wait before going back to patrol

    private Coroutine waitCoroutine;

    private void Start ( ) {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.instance;
       
    }

    void FootEvent ( int whichFoot ) {
        steps.Play();
    }

    private void Update ( ) {
        Animator.SetBool(ISWALKING, agent.velocity.magnitude > 0.01f);

        if(playermovement.IsRunning) {
            sightRange = 5;
        } else if(playermovement.isFullWalking) {
            sightRange = 4f;
        } else if(playermovement.isMediumWalking) {
            sightRange = 3f;
        } else if(playermovement.isSlowWalking) {
            sightRange = 2f;
        } else {
            sightRange = 0.5f;
        }

        offset = transform.position + new Vector3(0f,2f,0f);

        playerInSightRange = Physics.CheckSphere(offset, sightRange, whatIsPlayer);

        if(playerInSightRange) {
            timeSinceLastSeenPlayer = 0f; // Reset the timer
            if(waitCoroutine != null) {
                StopCoroutine(waitCoroutine);
                agent.isStopped = false;
            }
            ChasePlayer();
        } else {
            timeSinceLastSeenPlayer += Time.deltaTime;
            if(timeSinceLastSeenPlayer > timeToLosePlayer) {
                Patrol();
            }
        }

        if(Vector3.Distance(transform.position, target) < 1) {
            Patrol();
            IterateWaypointIndex();
        }
    }

    private void ChasePlayer ( ) {
        agent.SetDestination(player.transform.position);
        if(!evilMusic.isPlaying) {
            evilMusic.Play();
           
        }
    }

    void Patrol ( ) {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        evilMusic.Pause();
    }

    void IterateWaypointIndex ( ) {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length) {
            waypointIndex = 0;
        }

        // Check if this is the third waypoint
        if(waypointIndex % 3 == 0) {
            StopForTime();
        }
    }

    void StopForTime ( ) {
        if(waitCoroutine != null) {
            StopCoroutine(waitCoroutine);
        }
        // Start the wait coroutine
        waitCoroutine = StartCoroutine(WaitAtWaypoint(7f)); // Wait for 7 seconds (adjust as needed)
    }

    IEnumerator WaitAtWaypoint ( float waitTime ) {
        // Stop the agent
        agent.isStopped = true;
        Animator.SetBool(ISWALKING, false);

        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Resume the agent
        agent.isStopped = false;
        Animator.SetBool(ISWALKING, true);
        Patrol();  // Ensure the agent resumes patrolling

      /*  waitCoroutine = null;*/ // Clear the coroutine reference
    }




    private void OnTriggerEnter ( Collider other ) {
        if(other.gameObject.tag == "Player") {

        
            gameManager.GameOver();
            evilMusic.Pause();
        
        }
    }

    private void OnDrawGizmosSelected ( ) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(offset, sightRange);
    }
}
