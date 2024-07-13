using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EvilMrBeast : MonoBehaviour {

    public GameObject player;
    public NavMeshAgent agent;

    public LayerMask whatIsGround, whatIsPlayer;

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;
    private const string ISWALKING = "isWalking";
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource steps;
    [SerializeField] private AudioSource evilMusic;

    private GameManager gameManager;
    public FlashLightManager flashLight;
    [SerializeField] private FirstPersonMovement playermovement;

    // States
    public float sightRange;
    public bool playerInSightRange;

    private float lastPlayerSightTime = Mathf.NegativeInfinity; // Keeps track of the last time player was seen

    private void Start ( ) {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.instance;
    }

    void FootEvent ( int whichFoot ) {
        steps.Play();
    }

    private void Update ( ) {
        agent.destination = player.transform.position;
        Animator.SetBool(ISWALKING, agent.velocity.magnitude > 0.01f);

        if(playermovement.IsRunning) {
            sightRange = 9;
        } else if (playermovement.IsWalking()) {
            sightRange = 5;
        }
        else
        {
            sightRange = 3;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        // Update lastPlayerSightTime if player is in sight
        if(playerInSightRange) {
            lastPlayerSightTime = Time.time;
        }

        // Check if player is not in sight and enough time has passed
        if(!playerInSightRange && Time.time - lastPlayerSightTime > 4f) {
            Patrol();
        } else if(playerInSightRange) {
            ChasePlayer();
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
        // Start the wait coroutine
        StartCoroutine(WaitAtWaypoint(7f)); // Wait for 3 seconds (adjust as needed)
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
    }

    private void OnTriggerEnter ( Collider other ) {
        if(other.gameObject.tag == "Player") {
            // Call the GameOver function from GameManager
            gameManager.GameOver();
            evilMusic.Pause();
        }
    }

    private void OnDrawGizmosSelected ( ) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
