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
   
    

    //States
    public float sigthRange;
    public bool playerInSightRange;

    private void Start ( ) {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.instance;

    }




  void FootEvent(int whichFoot) {
        steps.Play();
    }

    private void Update ( ) {
        agent.destination = player.transform.position;
        Animator.SetBool(ISWALKING, agent.velocity.magnitude > 0.01f);

        if (flashLight.flashLightIsOn)
        {
            sigthRange = 7;
        }else {
            sigthRange = 2;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sigthRange, whatIsPlayer);

        
        if(!playerInSightRange) Patrol();
        if(playerInSightRange) ChasePlayer();

        if(Vector3.Distance(transform.position, target) < 1) {
            IterateWaypointIndex();
            Patrol();
        }


    }




    private void ChasePlayer() {
        agent.SetDestination(player.transform.position);
        if(!evilMusic.isPlaying) {
            evilMusic.Play();
        }
    }

    void Patrol ( ) {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex ( ) {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        if(other.gameObject.tag == "Player") {
            // Call the GameOver function from GameManager
            gameManager.GameOver();
        }
    }


    private void OnDrawGizmosSelected ( ) {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sigthRange);
    }
}
