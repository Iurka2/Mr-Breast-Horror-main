using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvilMrBeast : MonoBehaviour {
 
    public GameObject player;
    public NavMeshAgent agent;
    private const string ISWALKING = "isWalking";
    [SerializeField] private Animator Animator;
    [SerializeField] private AudioSource steps;
    private GameManager gameManager;

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
      
    }

    private void OnTriggerEnter ( Collider other ) {
        if(other.gameObject.tag == "Player") {
            // Call the GameOver function from GameManager
            gameManager.GameOver();
        }
    }
}
